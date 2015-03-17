using System;

class PontoRef{
    int x, y;
    public PontoRef(int x, int y){
        this.x = x;
        this.y = y;
    }
}

struct PontoVal{
    int x, y;
    
    public PontoVal(int x, int y){
        this.x = x;
        this.y = y;
    }
    
    public override bool Equals(Object o){
        PontoVal other = (PontoVal) o;
        return x == other.x && y == other.y;
    }

}

class App{
    static void Main(){
        PontoVal v1 = new PontoVal(5,7);
        PontoVal v2 = new PontoVal(5,7);
        Console.WriteLine(v1.Equals(v2));
        
        PontoRef r1 = new PontoRef(5,7);
        PontoRef r2 = new PontoRef(5,7);
        Console.WriteLine(r1.Equals(r2));
        
        string key = "ISEL";
        Console.Write("Introduza uma palavra: ");
        String word = Console.ReadLine();
        
        Console.WriteLine("Igualdade entre ISEL e {0} = {1}", word, key.Equals(word));
        // Errado: porque existe Overload de operator==
        // Console.WriteLine("Identidade entre ISEL e {0} = {1}", word, key == word);
        Console.WriteLine("Identidade entre ISEL e {0} = {1}", 
            word, 
            object.ReferenceEquals(key, word));
    }
}