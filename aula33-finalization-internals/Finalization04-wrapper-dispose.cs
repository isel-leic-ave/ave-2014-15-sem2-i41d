using System;
using System.IO;

static class App{
    class FileWrapper : IDisposable{
        FileStream fs; // Campo do tipo IDisposable
        bool disposed = false;
        
        public FileWrapper(string path)
        {
             fs = new FileStream(path, FileMode.Create);
        }
        
        public void Dispose(){
            if(!disposed) {
                fs.Dispose();
                GC.SuppressFinalize(this);
            }
            disposed = true;
        }
        ~FileWrapper(){
            Dispose();
        }        
    }

	public static void Main(){
        
        // 
        // <=> ao Try-with-resources do Java
        //
        using(FileWrapper fs = new FileWrapper("out.txt")){
            // FileWrapper fs = new FileWrapper("out.txt");
            // Wait for user to hit <Enter>
            Console.WriteLine("Wait for user to hit <Enter>");
            Console.ReadLine();
            // GC.Collect();
            // GC.WaitForPendingFinalizers();
        } 
        
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
        
	}
    
}
