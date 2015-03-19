using System;

class A{}
struct X{}

class App{
    static void Main(){
        A a = new A();
        Type aT = a.GetType();//ldloc.0 + callvirt Object::Gettype
        
        X x = new X();
        Type xT = x.GetType();//ldloc.2 + box + call Object::Gettype
    }
}