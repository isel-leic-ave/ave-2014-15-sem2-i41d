﻿using RestSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

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
        DowjonesEventHandler h2 = (t, uri, dt) => MessageBox.Show(String.Format("{0} ({1}): {2}", t, uri, dt));
        news.DowjonesEvent += h1; // != de += do Delegate.    => add_DowjonesEvent()
        news.DowjonesEvent += h2; // add_DowjonesEvent()
        news.DowjonesEvent += h2; // add_DowjonesEvent()
        news.Pull(); // Chamados e Removidos
        
        /* 
         * h2 deve ser removido como handler de DowjonesEvent porque não é mais 
         * usado no programa, i.e. não tem root references.
         */
        GC.Collect(); 
        
        news.Pull();
        
        h1.GetHashCode();
    }
}
    
public delegate void DowjonesEventHandler(String title, String desc, DateTime when);

public class DowjonesNews
{
    private readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private readonly RestClient client = new RestClient("http://api.monitr.com/api/v1");
    private DowjonesEventHandler handlers;
    
    public event DowjonesEventHandler DowjonesEvent{
        add{ 
            if(handlers != null)
                foreach(DowjonesEventHandler h in handlers.GetInvocationList())
                    if(h.Equals(value))
                        return;
            handlers += value; 
        }
        remove{ handlers -= value;}
    }
    
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
        if(handlers != null)
            foreach(DowjonesEventHandler h in handlers.GetInvocationList()){
                h(news.Title, news.Link, date);
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
