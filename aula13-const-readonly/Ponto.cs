using System;

public class Ponto{

	public int _x, _y;

	public Ponto(int x, int y)
	{
		this._x = x;
		this._y = y;
	}

    public int X{
        get{return _x;}
        set{
            if(value < 0)
                throw new ArgumentException ();
            _x = value;
        }
    }
    
	public double Module {	 
		get{
            return Math.Sqrt((double)_x*_x + _y*_y);
        }
	}

	public void Print(){
		Console.WriteLine("Version 2 Point (x = {0}, y = {1}))", _x, _y);
	}

    static void Main(){}
}