using System;

public class Ponto{

	// readonly <=> Java final
	public readonly int _z, _x, _y;

	public Ponto(int x, int y)
	{
		this._x = x;
		this._y = y;
		this._z = 0;
	}

	public double GetModule() {	 
		return Math.Sqrt((double)_x*_x + _y*_y + _z*_z);
	}

	public void Print(){
		Console.WriteLine("Version 2 Point (x = {0}, y = {1}), z = {2})", _x, _y, _z);
	}

}