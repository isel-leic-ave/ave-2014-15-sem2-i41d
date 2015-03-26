using System;
public class A
{
    public A() { }

    public void AWrite(string s)
    {
        Console.WriteLine(s);
    }

    public int Get5()
    {
        return 5;
    }

    public static void Xpto()
    {

    }
}

public class Ponto
{
    int x,y;

    public static int Multiply(int x,int y){
        return x * y;
    }

    public Ponto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }
}

public class Ponto3D : Ponto{
    int z;

    public Ponto3D(int x, int y, int z) : base(x,y)
    {
        this.z = z;
    }

    public int GetZ()
    {
        return z;
    }

    public static int Sum(int x, int y, int z)
    {
        return x + y + z;
    }

    private static int Xpto(int x, int y, int z)
    {
        return x * y - z;
    }

    private void InstanceXpto() { }
}