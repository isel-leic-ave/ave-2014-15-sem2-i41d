using System;


struct A {
    int n;
    public A(int n){
        this.n = n;
    }
}

struct B{
}

public class App 
{
    const string c1 = "OLA";
    const int c2 = 2387;
    // const A c3 = new A(7); // erro de compilação new -> call ao .ctor
    // const B c4 = new B(); // new -> initobj
    
    readonly B c5 = new B();
    
    static void Main()
    {
        
    }
}

