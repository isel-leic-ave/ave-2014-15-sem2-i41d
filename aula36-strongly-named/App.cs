using System;
using System.Linq;

public class App
{
    struct A
    {
        public override String ToString()
        {
            return "Wonderful A";
        }
    }
    
    static void Main(String [] args)
    {
        foreach(string a in args)
            Console.WriteLine(FooService.Apply<A>(a));
    }
}