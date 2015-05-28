using System;

class ServiceProvider {
    public ServiceProvider(){ Console.WriteLine("Init ServiceProvider.... "); }
    public void DoWork(){  /* Do something hard */ }
}

class Service {
    public Service(){ Console.WriteLine("Init Service.... "); }
    
    private WeakReference<ServiceProvider> provider =  
        new WeakReference<ServiceProvider>(null);
    
    /*
     * Lazy initialization.
     * !!!!! Not thread safe
     */
    private ServiceProvider GetProvider(){
        ServiceProvider res;
        if(!provider.TryGetTarget(out res) || res == null){
            res = new ServiceProvider();
            provider.SetTarget(res);
        }
        return res;
        
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