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

public class Alerter : IListener
{
    public void Step(int n)
    {
        MessageBox.Show("Alerter: " + n);
    }

}

public class Beeper: IListener
{
    public void Step(int n)
    {
        MessageBox.Show("Beeper: " + n);
    }
}


public class App 
{
    static void Main()
    {
        Counter c = new Counter(11);
        c.AddListener(new Receiver());
        c.AddListener(new Alerter());
        c.AddListener(new Beeper());
        c.Start(8);
    }
}

