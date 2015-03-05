using System;

class PontoApp{
	static Ponto MakePonto(){
		Ponto p = new Ponto(5, 7);
		p.Print();
		Console.WriteLine("p._x = {0}", p._x);
		return p;
	}

	static void M(Ponto p){
		int x  = p._x;
	}
	
	static void Dummy(){
		Console.WriteLine("Just dummy");
	}
	static void Main()
	{
		Dummy();
		MakePonto();
	}

}