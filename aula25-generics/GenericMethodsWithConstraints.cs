using System;
using System.Collections.Generic;

class Utils {


    public interface I {
        void Foo();
    }

    public static void AddNew<T>(List<T> l) where T : I, new()
    {
        l[0].Foo();
        l.Add(new T()); // Não é possível na JVM.
    }


    public static void InitNulls<T>(List<T> l, int size) where T : class
    {
        for (int i = 0; i < size; i++)
        {
            l.Add(null);
        }
    }

    public static void Init<T>(List<T> l, int size)
    {
        for (int i = 0; i < size; i++)
        {
            l.Add(default(T));
        }
    }

}

struct Point {
    int x, y;

    public Point(int n1, int n2) {
        x = n1;
        y = n2;
    }

    public override String ToString() {
        return string.Format("({0}, {1})", x, y);
    }
}

class Student { 
}

class Program {

    static void Print<T>(List<T> elems) {
        foreach (var item in elems)
        {
            String res = item == null ? "null" : item.ToString();
            Console.Write(res + ", ");
        }
        Console.WriteLine();
    }

    static void Main() {
        List<int> l1 = new List<int>();
        List<Point> l2 = new List<Point>();
        List<Student> l3 = new List<Student>();

        Utils.Init(l1, 7);
        Utils.Init(l2, 7);
        Utils.Init(l3, 7);

        Print(l1);
        Print(l2);
        Print(l3);

        //Utils.AddNew(l2); // Erro de compilação pq Point não tem um construtor sem parâmetros
    }

}