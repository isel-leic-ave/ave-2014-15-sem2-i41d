using System;
using System.IO;

static class App{

    static void PrintRunningGC(){
        Console.WriteLine("Running GC...");
        GC.Collect();
    }

	public static void Main(){
        
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, i.e. um ficheiro.
        //
        FileStream fs = new FileStream("out.txt", FileMode.Create);
		// Wait for user to hit <Enter>
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
		
        PrintRunningGC();
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
        
	}
    
}
