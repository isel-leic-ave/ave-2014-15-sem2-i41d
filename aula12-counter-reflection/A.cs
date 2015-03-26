using System;
using System.Reflection;
using System.Windows.Forms;

public class Receiver:IListener
{
    public void Step(int n)
    {
        Console.WriteLine("Receiver: " + n);
    }
}

