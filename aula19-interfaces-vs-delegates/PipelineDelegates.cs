using System;
using System.Collections.Generic;
using System.Reflection;

public delegate void ArrayOperationHandler (double[] values);

public sealed class ArithmeticPipeline
{
    List<ArrayOperationHandler> ops = new List<ArrayOperationHandler>();
    
    public ArithmeticPipeline(params ArrayOperationHandler[] arr){
        foreach(ArrayOperationHandler o in arr) ops.Add(o);
    }
    public void ExecuteAll(double[] startArray) 
    {
        foreach(ArrayOperationHandler o in ops)
            o(startArray);
    }
}

class App{
    public static void Xpto(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]*=2;
    }   
    
    private void Inc(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]++;
    }
    
    static void Print(double[] a){
        for(int i = 0; i < a.Length; i++)
            Console.Write(a[i] + " ");
        Console.WriteLine();
    }
    static void Main(){
        double[] src1 = {3.0, 7.0};
        double[] src2 = {1.0, 9.0, 11.0};
        double[] src3 = {9.0, 11.0};
        ArithmeticPipeline pipe = new ArithmeticPipeline(Xpto, new App().Inc);
        pipe.ExecuteAll(src1);
        Print(src1);
        pipe.ExecuteAll(src2);
        Print(src2);
        pipe.ExecuteAll(src3);
        Print(src3);
    }
}