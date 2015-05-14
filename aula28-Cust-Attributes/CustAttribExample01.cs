using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
class Transactional : Attribute
{}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
class MyAttribute : Attribute
{
    private string label;
    public MyAttribute(){}
    public MyAttribute(string l){label = l;}
    public int MyValue{get; set;}
}

[My("Ola")]
[My(MyValue = 5)]
class A
{

    [Transactional]
    void Xpto()
    {
    }
}

class Program
{
    static void Main()
    {

    }

}