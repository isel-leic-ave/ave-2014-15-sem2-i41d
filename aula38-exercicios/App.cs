using System;

interface I { void M();}

class A : I { 
   void I.M() { Console.Write("A"); }
   public virtual void M() { Console.Write("A"); } 
}
class B : A{ public virtual void M() { Console.Write("B"); } }
class C : B { public void M() { Console.Write("C"); } }
class Program{
  static void Main(string[] args){
    C c = new C();
    A a = c; B b = c; I i = c;
    a.M(); b.M(); c.M(); i.M();
  }
}
