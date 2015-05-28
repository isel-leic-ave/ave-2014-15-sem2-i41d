using System;
using System.IO;

class MyWriter:StreamWriter, IDisposable{

	public MyWriter(Stream stream):base(stream){}
    
    bool disposed = false;
    
    public new void Dispose(){
        if(!disposed){
            disposed = true;
            GC.SuppressFinalize(this);
            Console.WriteLine("Disposing " + this.GetHashCode());
            Flush();
            this.BaseStream.Dispose();
        }
    }
	~MyWriter(){
        Console.WriteLine("Finalizing " + this.GetHashCode());
		Dispose();
	}
}

static class App{

    public static void testWriterWithoutDisposeAndFlush(){
        StreamWriter writer = new StreamWriter(new FileStream("tempWithoutDisposeAndFlush.txt", FileMode.Create));
		writer.Write("Ola ");
        writer.WriteLine("Mundo!");
		writer.WriteLine("Ola Isel");
    }
    
    public static void testWriterWithoutDispose(){
        StreamWriter writer = new StreamWriter(new FileStream("tempWithoutDispose.txt", FileMode.Create));
		writer.Write("Ola ");
        writer.WriteLine("Mundo!");
		writer.WriteLine("Ola Isel");
        writer.Flush();
    }

    public static void testWriterWithoutFlush(){
        using(StreamWriter writer = new StreamWriter(new FileStream("tempWithoutFlush.txt", FileMode.Create))) {
            writer.Write("Ola ");
            writer.WriteLine("Mundo!");
            writer.WriteLine("Ola Isel");
        }
    }
 
    public static void testMyWriter(){
        StreamWriter writer = new MyWriter(new FileStream("tempMyWriter.txt", FileMode.Create));
		writer.Write("Ola ");
        writer.WriteLine("Mundo!");
		writer.WriteLine("Ola Isel");
        // SEM flush e sem dispose
        // contudo esperamos que o Finalize faça o seu trabalho
    }
    
    public static void testMyWriterWithDispose(){
        using(StreamWriter writer = new MyWriter(new FileStream("tempMyWriterWithDispose.txt", FileMode.Create))) {
            writer.Write("Ola ");
            writer.WriteLine("Mundo!");
            writer.WriteLine("Ola Isel");
        } // ((IDisposable) writer).Dispose()
    }
    
	public static void Main(){
        testWriterWithoutDisposeAndFlush();
        testWriterWithoutDispose();
        testWriterWithoutFlush();
        testMyWriter();
        testMyWriterWithDispose();
        
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.ReadLine();
	}
}