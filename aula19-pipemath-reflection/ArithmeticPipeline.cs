using System;
using System.Collections.Generic;
using System.Reflection;

public interface IArrayOperation
{
    void Execute(double[] values);
}

public interface Loader
{
    List<IArrayOperation> LoadOperations();
}

public class LoaderOfMethods : Loader
{
    Assembly module;

    public LoaderOfMethods(String file)
    {
        module = Assembly.LoadFrom(file);
    }

    public List<IArrayOperation> LoadOperations()
    {
        List<IArrayOperation> list = new List<IArrayOperation>();
        Type[] types = module.GetTypes();

        foreach (Type t in types)
        {
            if(t.IsAbstract) continue;
            MethodInfo[] methods = t.GetMethods();
            foreach (MethodInfo method in methods)
                if (equalsToExecute(method)){
                    if(method.IsStatic)
                        list.Add(new AdaptStaticMethod(method));
                    else 
                        list.Add(new AdaptInstanceMethod(t, method));
                }
        }

        return list;
    }

    private static Boolean equalsToExecute(MethodInfo handler)
    {
        MethodInfo executeMethod = typeof(IArrayOperation).GetMethod("Execute");
        if (executeMethod.ReturnType != handler.ReturnType) return false;
        
        ParameterInfo[] executeParameters = executeMethod.GetParameters();
        ParameterInfo[] handlerParameters = handler.GetParameters();
        if( executeParameters.Length != handlerParameters.Length) return false;
        
        for (int i = 0; i < executeParameters.Length; ++i)
        {
            if (!handlerParameters[i].ParameterType.IsAssignableFrom(executeParameters[i].ParameterType)) 
                return false;
        }
        return true;
    }
}

class AdaptInstanceMethod:IArrayOperation{
    Object target;
    MethodInfo handler;
    
    public AdaptInstanceMethod(Type targetKlass, MethodInfo handler){
        this.target = Activator.CreateInstance(targetKlass);
        this.handler = handler;
    }
    public void Execute(double[] values){
        handler.Invoke(target, new Object[]{values});
    }
}

class AdaptStaticMethod:IArrayOperation{
    MethodInfo handler;
    
    public AdaptStaticMethod(MethodInfo handler){
        this.handler = handler;
    }
    public void Execute(double[] values){
        handler.Invoke(null, new Object[]{values});
    }
}


public sealed class ArithmeticPipeline
{
    List<IArrayOperation> ops;
    
    public ArithmeticPipeline(List<IArrayOperation> ops){ this.ops = ops; }
    
    public void ExecuteAll(double[] startArray) 
    {
        foreach(IArrayOperation o in ops)
            o.Execute(startArray);
    }

    public static ArithmeticPipeline LoadPipeline(Loader loader)
    {
        return new ArithmeticPipeline(loader.LoadOperations());
    }

}

class App{
    static void Print(double[] a){
        for(int i = 0; i < a.Length; i++)
            Console.Write(a[i] + " ");
        Console.WriteLine();
    }
    static void Main(){
        double[] src1 = {3.0, 7.0};
        double[] src2 = {1.0, 9.0, 11.0};
        double[] src3 = {9.0, 11.0};
        ArithmeticPipeline pipe = ArithmeticPipeline.LoadPipeline(new LoaderOfMethods("Handlers.dll"));
        pipe.ExecuteAll(src1);
        Print(src1);
        pipe.ExecuteAll(src2);
        Print(src2);
        pipe.ExecuteAll(src3);
        Print(src3);
        
    }
}