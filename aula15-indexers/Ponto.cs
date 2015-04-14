using System;
using System.Collections.Generic;

public class Ponto{

	private int _x;

	public Ponto(int x, int y)
	{
		this._x = x;
		Y = y;
	}

    public virtual int X{
        get{return _x;}
        set{
            if(value < 0)
                throw new ArgumentException ();
            _x = value;
        }
    }
    
    public int Y{get; set; }
    
	public double Module {	 
		get{
            return Math.Sqrt((double)X*X + Y*Y);
        }
	}

	public void Print(){
		Console.WriteLine("Version 2 Point (x = {0}, y = {1}))", _x, Y);
	}

    static void Main(){
        Ponto p = new Ponto(5,11);
        int res1 = p.X;
        // int res2 = p.get_X(); // Método get_X marcado com specialname nao pode ser chamado explicitamente
        
        List<String> l = new List<String>();
        String s = l[3];
    }
}