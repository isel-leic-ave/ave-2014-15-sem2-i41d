using System;

interface I{}

public class Ponto : I
{
    int x,y;
    public Ponto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Ponto3D : Ponto{
    int z;
    public Ponto3D(int x, int y, int z) : base(x,y)
    {
        this.z = z;
    }
}

class App{
    static void Main(){
        Ponto p = new Ponto(11, 7);
        Ponto p3d = new Ponto3D(41, 17, 131);
        
        Console.WriteLine("p is Ponto = " + (p is Ponto));
        Console.WriteLine("p.GetType().IsSubclassOf(typeof(Ponto)) = " + p.GetType().IsSubclassOf(typeof(Ponto)));
        Console.WriteLine("typeof(Ponto).IsAssignableFrom(p.GetType()) = " + typeof(Ponto).IsAssignableFrom(p.GetType()));
        Console.WriteLine("p.GetType().IsSubclassOf(typeof(I)) = " + p.GetType().IsSubclassOf(typeof(I)));
        Console.WriteLine("typeof(I).IsAssignableFrom(p.GetType()) = " + typeof(I).IsAssignableFrom(p.GetType()));
        Console.WriteLine("p3d.GetType().IsSubclassOf(typeof(Ponto)) = " + p3d.GetType().IsSubclassOf(typeof(Ponto)));
        
    }
}