using System;
using System.Reflection;

class Student{
    int nr;
    string name;
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

class Logger{

    public static void Log(object obj){
        Type objType = obj.GetType();
        FieldInfo[] fields = objType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        Console.Write(objType.Name + "[ ");
        foreach (FieldInfo f in fields)
        {
            String res = "";
            if(f.IsStatic)
                res = f.Name + ": " + f.GetValue(null) + ", "; // target null pq o campo é de classe/estático
            else
                res = f.Name + ": " + f.GetValue(obj) + ", ";
            Console.Write(res);
        }
        Console.WriteLine("]");
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