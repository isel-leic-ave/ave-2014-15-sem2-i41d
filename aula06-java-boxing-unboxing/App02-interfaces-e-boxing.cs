using System;

interface Setter{
    void SetRandomNr(int n);
}
struct Student:Setter{
    public int nr;
    public void SetRandomNr(int n){
        this.nr = n;
    }
}

class Program{

     private static Random rand = new Random();
     public static void SetNr(Setter n){
            n.SetRandomNr(rand.Next(1000));
     }
   
	static void Main(){
        Setter s = new Student(); // init da struct em Stack + boxing + stloc
        SetNr(s);
        int res = ((Student) s).nr;
        Console.WriteLine(res);
        
        // Limpar student
        // ((Student) s).nr = 0; // Erro de compilação => não é possivel alterar um campo de uma instancia em estado unbox.
        ((Student) s).SetRandomNr(0); // cast é desnecessário
        res = ((Student) s).nr;
        Console.WriteLine(res);
        
     }
}