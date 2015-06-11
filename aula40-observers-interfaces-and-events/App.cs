using System;
using System.Diagnostics;
using System.Collections.Generic;


public class Alarm{
  private List<Observer> obs = new List<Observer>();

  public void AddObserver(Observer o){ obs.Add(o); }
  public void RemoveObserver(Observer o) { obs.Remove(o);}
  public void NotifyAll(DateTime current) {
     foreach (Observer o in obs)
       o.Notify(current);
  }
  public void Start(long timeoutInMilis) {
     Stopwatch watch = Stopwatch.StartNew();
     do { } while (watch.ElapsedMilliseconds < timeoutInMilis);
     NotifyAll(DateTime.Now);
  }
}

delegate void Observer(DateTime current);

class ConsoleWriter : Observer {
    public void Notify(DateTime current) {
        Console.WriteLine("current: " + current);
    }
}

class Program {
    static void Main(string[] args) {
        Alarm a = new Alarm();
        a.AddObserver(new ConsoleWriter());
        a.Start(2000);
    }
}

