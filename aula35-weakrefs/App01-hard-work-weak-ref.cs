using System;

class ServiceProvider {
    public ServiceProvider(){ Console.WriteLine("Init ServiceProvier.... "); }
    public void DoWork(){  /* Do something hard */ }
}

class Service {
    public Service(){ Console.WriteLine("Init Service.... "); }
    
    private WeakReference provider =  new WeakReference(null);
    
    /*
     * Lazy initialization.
     * !!!!! Not thread safe
     */
    private ServiceProvider GetProvider(){
        if(!provider.IsAlive || provider.Target == null)
            provider.Target = new ServiceProvider();
        return (ServiceProvider) provider.Target;
    }
    
    public void DoWork(){
        Console.WriteLine("Requesting Work to ServiceProvider....");
        GetProvider().DoWork();
    }
}

static class App{
    static void Main(){
        Service s = new Service();
        s.DoWork(); // instancia o ServiceProvider
        s.DoWork();
        s.DoWork();
        
        GC.Collect();
        s.DoWork(); // s is root reference
        s.DoWork();
    }
}