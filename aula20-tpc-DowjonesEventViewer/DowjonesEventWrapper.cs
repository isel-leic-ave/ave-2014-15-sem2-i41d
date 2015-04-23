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
        // Adds handler as a subscriber to the DowjoneNews news object

        throw new NotSupportedException("Not implemented yet!");
    }

    public void RemoveHandler(DowjonesEventHandler handler)
    {
        // Removes handler from the DowjoneNews news object

        throw new NotSupportedException("Not implemented yet!");
    }

    
}
