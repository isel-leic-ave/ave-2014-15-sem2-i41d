using System;

interface I{ 
    void m(); // método abstracto e virtual e public
}
class A : I{   
    public virtual void m(){
        Console.WriteLine("A");
    }
}
class B: A, I{
    public new virtual void m(){
        Console.WriteLine("B");
    }
}
class C: B{
    public override void m(){
        Console.WriteLine("C");
    }
}

// ERRO de compilação NÃO podemos Reduzir a Acessibilidade
// class B : A{ protected override void m(){    } }

class App{
    static void Main(){
        C c = new C();
        A a = c; B b = c; I i = c;
        i.m(); a.m(); b.m(); c.m();
    }
}