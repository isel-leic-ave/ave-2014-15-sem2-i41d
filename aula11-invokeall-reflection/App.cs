using System;
using System.Reflection;
public class App{
    public static void InvokeAll(String path)
    {
        Assembly asm = Assembly.LoadFrom(path);
        Type[] types = asm.GetTypes();
        foreach(Type t in types){
            InvokeAll(t);
        }
    }

    private static void InvokeAll(Type t)
    {
        Console.WriteLine(t.Name);
        MethodInfo[] methods = t.GetMethods(BindingFlags.Instance |
                                            BindingFlags.Public|
                                            BindingFlags.DeclaredOnly|
                                            BindingFlags.Static);
        foreach (MethodInfo m in methods)
        {
            InvokeMethod(m, t);
        }
    }

    private static void InvokeMethod(MethodInfo m, Type t)
    {
        Console.WriteLine("\t"+m.Name+" :");
        Object target = GetInstance(t.GetConstructors()[0]); // Cria uma instancia via o 1º construtor devolvido
        Object[] parameters = GetDefaultValuesForParameters(m.GetParameters());
        Object result = m.Invoke(target, parameters);
        if (m.ReturnType != typeof(void))
            Console.WriteLine("\t\t" + result);
    }

    private static Object GetInstance(ConstructorInfo c)
    {
        object[] parameters = GetDefaultValuesForParameters(c.GetParameters());
        return c.Invoke(parameters);
    }

    private static object[] GetDefaultValuesForParameters(ParameterInfo[] paramTypes)
    {
        Object[] parameters = new Object[paramTypes.Length];
        for (int i = 0; i < paramTypes.Length; i++)
        {
            parameters[i] = GetDefaultValue(paramTypes[i]);
        }
        return parameters;
    }

    private static Object GetDefaultValue(ParameterInfo p)
    {
        Type t = p.ParameterType;
        ConstructorInfo[] cons = t.GetConstructors();
        if (cons.Length > 0)
            return GetInstance(cons[0]); // Obtem o 1º construtor
        if (t.IsValueType)
            return Activator.CreateInstance(t);
        return null;
    }

    public static void Main()
    {
        InvokeAll("A.dll");
    }
}
