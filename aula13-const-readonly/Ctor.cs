using System;
using System.Collections.Generic;

class A {

    static A someA = new A(9);
    static List<String> words;
    
    static A(){
        words = new List<String>();
        words.Add("Ola");
        words.Add("ISEL");
    }
    
    int n;
    int x = 8; // Código IL da afectação copiado para os 2 construtores
    
    public A(int n){
        this.n = n;
    }
    
    public A(){
        
    }
}

public class App 
{
    
    static void Main()
    {
        
    }
}

