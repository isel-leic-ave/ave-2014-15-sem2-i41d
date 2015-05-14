using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

public delegate void ArrayOperationHandler (double[] values);

public sealed class ArithmeticPipeline
{
    ArrayOperationHandler ops;
    
    public ArithmeticPipeline(params ArrayOperationHandler[] arr){
        foreach(ArrayOperationHandler o in arr) ops += o;
        // arr.Foreach(handler => ops += o); // Side Effects !!!! perigoso
    }
    public void ExecuteAll(double[] startArray) 
    {
        ops(startArray);
    }
}

public interface Loader
{
    List<ArrayOperationHandler> LoadOperations();
}

public class LoaderOfMethods : Loader
{
    Assembly module;

    public LoaderOfMethods(String file)
    {
        module = Assembly.LoadFrom(file);
    }

    public List<ArrayOperationHandler> LoadOperations()
    {
        List<ArrayOperationHandler> list = new List<ArrayOperationHandler>();
        Type[] types = module.GetTypes();

        foreach (Type t in types)
        {
            if(t.IsAbstract) continue;
            MethodInfo[] methods = t.GetMethods();
            foreach (MethodInfo method in methods)
                if (equalsToExecute(method)){
                    if(method.IsStatic)
                        list.Add(new ArrayOperationHandler(new AdaptStaticMethod(method).Execute));
                    else 
                        list.Add(new AdaptInstanceMethod(t, method).Execute);
                }
        }
        return list;
    }

    private static Boolean equalsToExecute(MethodInfo handler)
    {
        MethodInfo executeMethod = typeof(ArrayOperationHandler).GetMethod("Invoke");
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

class AdaptInstanceMethod{
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

class AdaptStaticMethod{
    MethodInfo handler;
    
    public AdaptStaticMethod(MethodInfo handler){
        this.handler = handler;
    }
    public void Execute(double[] values){
        handler.Invoke(null, new Object[]{values});
    }
}

class App{
    public static void Duplicate(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]*=2;
    }   
    
    private void Inc(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]++;
    }
    
    static void Print(double[] a){
        for(int i = 0; i < a.Length; i++)
            Console.Write(a[i] + " ");
        Console.WriteLine();
    }
    
    static void Main(){
        double[] src1 = {3.0, 7.0};
        double[] src2 = {1.0, 9.0, 11.0};
        double[] src3 = {9.0, 11.0};
        
        LoaderOfMethods loader = new LoaderOfMethods("Handlers.dll");
        ArithmeticPipeline pipe = new ArithmeticPipeline(loader.LoadOperations().ToArray());
        
        pipe.ExecuteAll(src1);
        Print(src1);
        
        pipe.ExecuteAll(src2);
        Print(src2);
        
        pipe.ExecuteAll(src3);
        Print(src3);
    }
}