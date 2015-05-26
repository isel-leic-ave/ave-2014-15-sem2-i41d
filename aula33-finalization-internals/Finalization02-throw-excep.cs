using System;
using System.IO;

static class App{

    class FileStreamClean : FileStream{
        public FileStreamClean(string path) : base(path, FileMode.Create) {}
        ~FileStreamClean(){ throw new Exception("Breaking Finalize"); }
    }

    static void PrintRunningGC(){
        Console.WriteLine("Running GC...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

	public static void Main(){
        
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, i.e. um ficheiro.
        //
        FileStream fs = new FileStreamClean("out.txt");
		// Wait for user to hit <Enter>
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
		
        PrintRunningGC();
        
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
        
	}
    
}
