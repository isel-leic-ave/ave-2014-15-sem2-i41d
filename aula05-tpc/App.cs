using System;

class Program{

    static void Print(short n){
    
    }


    static void conv1(long n1) {
        Console.WriteLine("Conv 1");
        short n2 = (short)n1; // coercao explicita => conv.i2
        Console.WriteLine(n2);
        Print(n2);
        // Print(n1); => Erro de compilacao
    }

    static void conv2(long n1) {
        checked
        {
            Console.WriteLine("Conv 2");
            short n2 = (short)n1;// coercao explicita => conv.ovf.i2
            Console.WriteLine(n2);
            int res = n2 + 76576;
        }
    }

	static void Main(){
        conv1(50000); // coercao implicita => conv.i8
        conv2(50000); // coercao implicita => conv.i8   
	}
}