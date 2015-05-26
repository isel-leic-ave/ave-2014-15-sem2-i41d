using System;
using System.IO;

static class App{


    // FileStreamClean ---|> FileStream ---|> IDisposable
    class FileStreamClean : FileStream{
        public FileStreamClean(string path) : base(path, FileMode.Create) {}
        ~FileStreamClean(){ throw new Exception("Breaking Finalize"); }
    }

	public static void Main(){
        
        // 
        // <=> ao Try-with-resources do Java
        //
        using(FileStream fs = new FileStreamClean("out.txt")){
            // Wait for user to hit <Enter>
            Console.WriteLine("Wait for user to hit <Enter>");
            Console.ReadLine();
        } 
        
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
        
	}
    
}
