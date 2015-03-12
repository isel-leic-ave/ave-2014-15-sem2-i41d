using System;

class Program{

    static void Print(Object o){
        Console.WriteLine(o);
    }

    static void Print(int n){
        Console.WriteLine(n);
    }
    
	static void Main(){
        String s1 = "Ola";
        int n1 = 68;
        Print(s1);
        Print(n1);
        Print(n1);
        
        // Imaginem que continuava o programa com qq coisa....
	}
}