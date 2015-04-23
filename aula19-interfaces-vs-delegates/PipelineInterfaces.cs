using System;
using System.Collections.Generic;
using System.Reflection;

public interface IArrayOperation{ void Execute(double[] values); }

public sealed class ArithmeticPipeline
{
    List<IArrayOperation> ops = new List<IArrayOperation>();
    
    public ArithmeticPipeline(params IArrayOperation[] arr){
        foreach(IArrayOperation o in arr) ops.Add(o);
    }
    public void ExecuteAll(double[] startArray) 
    {
        foreach(IArrayOperation o in ops)
            o.Execute(startArray);
    }
}

class App{
    class Dup : IArrayOperation{
        public void Execute(double[] a){
            for(int i = 0; i < a.Length; i++)
                a[i]*=2;
        }   
    }
    class Inc : IArrayOperation{        
        public void Execute(double[] a){
            for(int i = 0; i < a.Length; i++)
                a[i]++;
        }
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
        ArithmeticPipeline pipe = new ArithmeticPipeline(new Dup(), new Inc());
        pipe.ExecuteAll(src1);
        Print(src1);
        pipe.ExecuteAll(src2);
        Print(src2);
        pipe.ExecuteAll(src3);
        Print(src3);
    }
}