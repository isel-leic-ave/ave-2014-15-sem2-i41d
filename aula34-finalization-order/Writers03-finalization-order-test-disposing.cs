using System;
using System.IO;

class MyWriter : StreamWriter, IDisposable{
    bool disposed = false;

	public MyWriter(Stream stream):base(stream){}
    
    public MyWriter(String path):base(new FileStream(path, FileMode.Create)){ }

    public new void Dispose(){
        Dispose(true);
    }
    
	private new void Dispose(bool disposing){
        if(!disposed){
            disposed = true;
            Console.WriteLine("Disposing " + this.GetHashCode());
            if(disposing) {
                /*
                 * Zona Safe de acesso a recursos "Finalizable"
                 */
                GC.SuppressFinalize(this);
                Flush();
                this.BaseStream.Dispose();
            }
        }
    }
	~MyWriter(){
        Console.WriteLine("Finalizing " + this.GetHashCode());
		Dispose(false);
	}
}
static class Program{
    
    public static void testMyWriterEager(){
        StreamWriter writer = new MyWriter(new FileStream("tempEager.txt", FileMode.Create));
		writer.Write("Ola ");
        writer.WriteLine("Mundo!");
		writer.WriteLine("Ola Isel");
        // SEM flush e sem dispose
        // contudo esperamos que o Finalize faça o seu trabalho
    } 
    
    public static void testMyWriterLazy(){
        StreamWriter writer = new MyWriter("tempLazy.txt"); // Vai ser o MyWriter a instanciar o Filestream
		writer.Write("Ola ");
        writer.WriteLine("Mundo!");
		writer.WriteLine("Ola Isel");
        // SEM flush e sem dispose
        // contudo esperamos que o Finalize faça o seu trabalho
    } 
    
    public static void testMyWriterEagerDispose(){
        using( StreamWriter writer = new MyWriter(new FileStream("tempEager.txt", FileMode.Create))) {
            writer.Write("Ola ");
            writer.WriteLine("Mundo!");
            writer.WriteLine("Ola Isel");
        }
    } 
    
    public static void testMyWriterLazyDispose(){
        using( StreamWriter writer = new MyWriter("tempLazy.txt")) {  // Vai ser o MyWriter a instanciar o Filestream
            writer.Write("Ola ");
            writer.WriteLine("Mundo!");
            writer.WriteLine("Ola Isel");
        }
    } 
    
	public static void Main(){
        testMyWriterEager();
        // testMyWriterEagerDispose();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.ReadLine();
        
        testMyWriterLazy();
        // testMyWriterLazyDispose();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.ReadLine();
        
	}
}