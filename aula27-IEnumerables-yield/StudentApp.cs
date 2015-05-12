using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Student
{
    public readonly int nr;
    public readonly String name;
    public readonly int selected;
    public readonly int grade;

    public Student(int nr, String name, int selected, int grade)
    {
        this.nr = nr;
        this.name = name;
        this.selected = selected;
        this.grade = grade;
    }

    public override String ToString()
    {
        return String.Format("{0} {1} ({2}, {3})", nr, name, selected, grade);
    }
    public void Print()
    {
        Console.WriteLine(this);
    }
    
    public static Student Parse(string src){
        string [] words = src.Split('|');
        return new Student(
            int.Parse(words[1]),
            words[2],
            int.Parse(words[3]),
            int.Parse(words[4]));
    }
}

static class App
{
    private static readonly String STUDENTS_FILE = "..\\00-raffle\\isel-ave-2014-15-sem2-listagem.txt";

    static IEnumerable<String> WithLines(string path)
    {
        string line;
        using (StreamReader file = new StreamReader(path, Encoding.UTF8)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }

    static IEnumerable<R> Select<T, R>(this IEnumerable<T> src, Func<T, R> proj){
        foreach(T item in src){
            yield return proj(item);
        }
    }
    static IEnumerable<T> Where<T>(this IEnumerable<T> src, Predicate<T> p){
        foreach(T item in src){
            if(p(item)) yield return item;
        }
    }
    static IEnumerable<T> Top<T>(this IEnumerable<T> src, int total){
        int i = 0;
        foreach(T item in src){
            if(i == total) break;
            i++;
            yield return item;
        }
    }
    static IEnumerable<T> Distinct<T>(this IEnumerable<T> src){
        HashSet<T> values = new HashSet<T>();
        foreach(T elem in src){
            if(!values.Contains(elem)){
                values.Add(elem);
                yield return elem;
            }
        }
    }
    static void Foreach<T>(this IEnumerable<T> src, Action<T> a){
        foreach(T item in src){
            a(item);
        }
    }
    static void Print(string label, Object item){
        Console.WriteLine(label + ": " + item);
    }
    
    static void Main()
    {
        WithLines(STUDENTS_FILE)
            .Select(Student.Parse)
            .Select(s => s.name.Split(' ')[0])
            .Distinct()
            .Top(10)
            .Foreach(Console.WriteLine);
    }
}