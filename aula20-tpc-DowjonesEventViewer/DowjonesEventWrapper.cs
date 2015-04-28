using System;

public delegate void DowjonesEventHandler(String title, String desc, DateTime when);

public class DowjonesEventWrapper
{
    private DowjonesNews news = new DowjonesNews();

    public void Pull() 
    {
        news.Pull();
    }

    public void AddHandler(DowjonesEventHandler handler)
    {
        news.AddSubscriber(new HandlerAdapter(handler));
    }

    public void RemoveHandler(DowjonesEventHandler handler)
    {
        news.RemoveSubscriber(new HandlerAdapter(handler));
    }    
}

class HandlerAdapter : Subscriber {

    private DowjonesEventHandler handler;

    public HandlerAdapter(DowjonesEventHandler h){ handler = h; }

    public void Occurrence(string title, string uri, DateTime when) {
        handler(title,uri,when);
    }

    public override bool Equals(object obj) {
        if (obj == null) {
            return false;
        }
        HandlerAdapter toCompare = obj as HandlerAdapter;
        return this.handler.Equals(toCompare.handler);
    }

    public override int GetHashCode() {
        return this.handler.GetHashCode();
    }
}
