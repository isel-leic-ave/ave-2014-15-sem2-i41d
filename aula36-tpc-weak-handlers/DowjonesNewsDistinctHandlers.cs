using RestSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

class AppViewer
{
    public static void ViewOnConsole(string title, string uri, DateTime when)
    {
        Console.WriteLine("{0} ({1}): {2}", title, uri, when);
    }
    static void Main(string[] args)
    {
        DowjonesNews news = new DowjonesNews();
        DowjonesEventHandler h1 = ViewOnConsole;
        DowjonesEventHandler h2 = WorkingFixToH2;
        news.DowjonesEvent += h1; // != de += do Delegate.    => add_DowjonesEvent()
        news.DowjonesEvent += h2; // add_DowjonesEvent()
        news.DowjonesEvent += h2; // add_DowjonesEvent()
        news.Pull(); 
        Console.WriteLine(news.HandlersCount);
        
        /* 
         * h2 deve ser removido como handler de DowjonesEvent porque não é mais 
         * usado no programa, i.e. não tem root references.
         */
        GC.Collect(); 
        
        news.Pull();
        
        Console.WriteLine(news.HandlersCount);
        h1.GetHashCode();
    }

    static void WorkingFixToH2(string title, string uri, DateTime when){
         MessageBox.Show(String.Format("{0} ({1}): {2}", title, uri, when));
    }
}
    
public delegate void DowjonesEventHandler(String title, String desc, DateTime when);

public class DowjonesNews
{
    private readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private readonly RestClient client = new RestClient("http://api.monitr.com/api/v1");
    private readonly HashSet<WeakReference<DowjonesEventHandler>> handlersList = new HashSet<WeakReference<DowjonesEventHandler>>(new RefComparer());
    
    public event DowjonesEventHandler DowjonesEvent{
        add{handlersList.Add(new WeakReference<DowjonesEventHandler>(value));}
        remove { handlersList.Remove(new WeakReference<DowjonesEventHandler>(value)); }
       
    }
    
    public int HandlersCount { get{ return handlersList.Count; }}
    
    public void Pull()
    {
        RestRequest request = new RestRequest("/market/news?apikey=1e3f8640-f754-11e3-97e9-179fff8a3cc5&max=1", Method.GET);
        MonitrResp resp = client.Execute<MonitrResp>(request).Data;
        if (resp.Data.Count == 0) throw new InvalidOperationException("Empty response from Monitr!");
        NotifySubscribers(resp.Data[0]);
    }

    void NotifySubscribers(MonitrRespData news)
    {
        DateTime date = start.AddMilliseconds(news.Time).ToLocalTime();

        List<WeakReference<DowjonesEventHandler>> toClean = new List<WeakReference<DowjonesEventHandler>> ();
        foreach (WeakReference<DowjonesEventHandler> h in handlersList)
        {
            DowjonesEventHandler res;
            if (h.TryGetTarget(out res) && res != null)
            {
                res(news.Title, news.Link, date);
            } else {
                toClean.Add(h);
            }
        }
        
        foreach (WeakReference<DowjonesEventHandler> h in toClean) {
            handlersList.Remove(h);
        }
    }
}

class MonitrResp
{
    public int Status { get; set; }
    public List<MonitrRespData> Data { get; set; }
}

class MonitrRespData
{
    public string Market { get; set; }
    public string Title { get; set; }
    public string Symbol { get; set; }
    public string Link { get; set; }
    public long Time { get; set; }
    public string Domain { get; set; }

}

class RefComparer : IEqualityComparer<WeakReference<DowjonesEventHandler>>
{
    public bool Equals(WeakReference<DowjonesEventHandler> ref1, WeakReference<DowjonesEventHandler> ref2)
    {
        DowjonesEventHandler handler1;
        bool ref1HaveTarget = ref1.TryGetTarget(out handler1) && handler1 != null;
        DowjonesEventHandler handler2;
        bool ref2HaveTarget = ref2.TryGetTarget(out handler2) && handler2 != null;

        if (ref1HaveTarget && ref2HaveTarget) return handler1.Equals(handler2);
        else if (!ref1HaveTarget && !ref2HaveTarget) return true;
        else return false;
    }


    public int GetHashCode(WeakReference<DowjonesEventHandler> arg)
    {
        DowjonesEventHandler handler;
        return arg.TryGetTarget(out handler) && handler != null ? handler.GetHashCode() : 0;
    }
}