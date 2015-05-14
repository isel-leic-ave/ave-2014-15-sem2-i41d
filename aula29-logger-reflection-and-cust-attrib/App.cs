using System;
using System.Reflection;


delegate string LogFormatterHandler(object o);

class LogField : Attribute {
    private LogFormatterHandler formatter;
    
    public LogField(){}
    
    public LogField(Type t){
        foreach (MethodInfo method in t.GetMethods())
            if (equalsToExecute(method)){
                object target = method.IsStatic ? null : Activator.CreateInstance(t);
                formatter = arg => (string) method.Invoke(target, new object[]{arg});
            }
    }
    public string Format(object v){
        if(formatter == null)  return v.ToString();
        else return formatter(v);
    }
    
     private static bool equalsToExecute(MethodInfo handler)
    {
        MethodInfo executeMethod = typeof(LogFormatterHandler).GetMethod("Invoke");
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

class Logger{

    public static void Log(object obj){
        Type objType = obj.GetType();
        FieldInfo[] fields = objType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        Console.Write(objType.Name + "[ ");
        foreach (FieldInfo f in fields)
        {
            LogField[] attrs = (LogField[]) f.GetCustomAttributes(typeof(LogField), true);
            if(attrs.Length != 0){
                object target = f.IsStatic ? null : obj;
                object fieldValue = f.GetValue(target);
                Console.Write(f.Name + ": " + attrs[0].Format(fieldValue) + ", ");
            }
        }
        Console.WriteLine("]");
    }
}


class NameFormatter{
    public static string Format(object name){
        return name.ToString().ToUpper();
    }
}

class Student{
    [LogField] int nr;
    [LogField(typeof(NameFormatter))] string name;
    public static int totalOfStudents;
    public Student(int nr, string name) {
        this.nr = nr;
        this.name = name;
        Student.totalOfStudents++;
    }
}

class Person{
    string id;
    string nif;
    public Person(string id, string nif) {
        this.id = id;
        this.nif = nif;
    }
}

class Program{

	static void Main(){
        Student s1 = new Student(2323, "Ze Manel");
        Student s2 = new Student(67765, "Maria Rosa");
        Person p1 = new Person("yu3t13", "348534553");
        Logger.Log(s1);
        Logger.Log(s2);
        Logger.Log(p1);
        
     }
}