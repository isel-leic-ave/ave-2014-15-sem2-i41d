using System;
using System.Windows.Forms;
using System.Collections.Generic;

class Program
{
    String Foo(){ Console.WriteLine("Ola Cheguei"); return "Ola Cheguei"; }
    static String Bar(String label){ 
        string res = label + ": Ola Cheguei";
        Console.WriteLine(res);
        return res; 
    }

    static void Main()
    {
        Func<String, String> h1 = Program.Bar;
        Func<double> h2 = () => { Console.WriteLine(Math.PI); return Math.PI; };
        Func<double> h3 = () => { Console.WriteLine(Math.Sqrt(2)); return Math.Sqrt(2); };
        Func<String> h4 = (new Program()).Foo;

        MultiDelegator md = new MultiDelegator();
        md.AddHandler(h1);
        md.AddHandler(h2);
        md.AddHandler(h3);
        md.AddHandler(h4);
        md.DispatchAndPrint(typeof(Func<String>));
        md.DispatchAndPrint(typeof(Func<double>));
        md.RemoveHandler(h3);
        md.DispatchAndPrint(typeof(Func<double>));
        md.RemoveHandler(h2);
        md.DispatchAndPrint(typeof(Func<double>));
        // md.DispatchAndPrint(typeof(Func<String, String>)); // Excepção o DispatchAndPrint não passa argumentos ao delegate
    }
}
class MultiDelegator
{
    private Dictionary<Type, Delegate> handlers = new Dictionary<Type, Delegate>();
    public void AddHandler(Delegate h) {
        Delegate aux;
        if(handlers.TryGetValue(h.GetType(), out aux))
        {
            aux = Delegate.Combine(aux, h); //aux += h; // !!!1 Erro e compilação nao sabe o tipo destino do cast
            handlers[h.GetType()] = aux; 
        }
        else
            handlers.Add(h.GetType(), h);        
    }
    public void RemoveHandler(Delegate h)
    {
        Delegate aux;
        if(handlers.TryGetValue(h.GetType(), out aux))
        {
            aux = Delegate.Remove(aux, h);
            handlers[h.GetType()] = aux; 
        }
    }
    public void DispatchAndPrint(Type t) {
        Delegate aux;
        if(handlers.TryGetValue(t, out aux))
        {
            if(aux != null) aux.DynamicInvoke();
        }
    }

}