using System;

interface Setter{
    void SetRandomNr(int n);
}


struct Student:Setter{
    int nr;
    public void SetRandomNr(int n){
        this.nr = n;
    }
}

// struct Teacher:Person{} // Erro de compilação => so aceita interface
// class  Teacher:System.ValueType{} // ValueType é um tipo especial e não pode ser usado pelo programador.
// struct Teacher:System.ValueType{} // Erro de compilação => so aceita interface

class Person: Setter{
    string id;
    
    public void SetRandomNr(int n){
        this.id = n.ToString(); 
    }
}
class Program{

     private static Random rand = new Random();
     public static void SetNr(Setter n){
            n.SetRandomNr(rand.Next(1000));
     }
   
	static void Main(){
        Student s = new Student();
        Person p = new Person();
        SetNr(s); // => boxing => T.V. => TR (interface)
        SetNr(p); // => upcast
        
     }
}