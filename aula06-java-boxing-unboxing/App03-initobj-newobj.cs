using System;

interface Setter{
    void SetRandomNr(int n);
}

// Não se pode definir construtores sem parametros em Struct
struct Student:Setter{
    public int nr;
    private String name;
    
    // error CS0568: Structs cannot contain explicit
    //    parameterless constructors
    // public Student(){}
    
    public Student(string str){
        this.nr = 999;
        this.name = str;
    }
    
    public void SetRandomNr(int n){
        this.nr = n;
    }
}

class Person{
    private string id, name;
}
class Teacher:Person{
    string dep;
    // public Teacher():base(){
    public Teacher(){// implicito a chamada ao ctor da base
        dep = "ADEETC";
    }
}

class Group {
    Student s1, s2, s3; // armazenados in-place
}

class Program{

     private static Random rand = new Random();
     public static void SetNr(Setter n){
            n.SetRandomNr(rand.Next(1000));
     }
   
	static void Main(){
        Student s = new Student(); // init da struct em Stack + boxing + stloc
        Person p = new Person();
    }
}