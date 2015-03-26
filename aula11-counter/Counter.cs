using System;
using System.Reflection;
using System.Collections.Generic;


public interface IListener
{
    void Step(int n);
}

public class Counter{
    int end;
    List<IListener> listeners = new List<IListener>();

    public Counter(int end)
    {
        this.end = end;
    }

    public void Start(int from) {
        for (int i = from; i <= end; i++)
        {
            notifyAll(i);
        }
    }

    private void notifyAll(int val) 
    {
        foreach (IListener l in listeners)
        {
            l.Step(val);
        }
    }

    public void AddListener(IListener l)
    {
        listeners.Add(l);
    }
}
