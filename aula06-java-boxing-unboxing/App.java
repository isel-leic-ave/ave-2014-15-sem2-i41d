public class App{

    public static void main(String[] args){
    
        // comportamento igual em Java ou C#
        int n1 = 897;
        Object o1 = n1; // boxing
        int n2 = (int) o1; //unboxing
        
        Integer n3 = n1; // <=> Integer.valueOf(n1) --- boxing
        
        int n4 = n3; // operação de unboxing implícita != C# 
        // <=> int n4 = n3.intValue();
    }
}
