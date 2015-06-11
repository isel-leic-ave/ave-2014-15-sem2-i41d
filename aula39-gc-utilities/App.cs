using System;
using System.Threading;
class obj_space{	
	static void Main(string[] args)
		{
		int i = 0;
		Holder prev = new Holder(null);
		while(GC.CollectionCount(1) == 0)
		{
			++i;
			Holder h = new Holder(prev);
			prev = h;
		}
		Console.WriteLine((i+1)*8);
		Console.ReadLine();
	}
}

class Holder{
	Holder prev;
	public Holder(Holder h){
		this.prev = h;
	}
}

