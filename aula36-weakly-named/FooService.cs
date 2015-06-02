extern alias FooServiceReal;

using System;
using System.IO;

public class FooService 
{
    public static T Apply<T>(object src)
    {        
        using(StreamWriter fs = new StreamWriter(new FileStream("out.txt", FileMode.Append)) ) {
            fs.WriteLine(src);
            return FooServiceReal::FooService.Apply<T>(src);
        }   
    }
}