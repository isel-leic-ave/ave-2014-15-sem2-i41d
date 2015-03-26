using System;
using System.Reflection;
using System.Windows.Forms;


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
    }
}

