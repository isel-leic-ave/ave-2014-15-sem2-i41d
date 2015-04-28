using System;
using System.Windows.Forms;
    
class AppViewer
{
    public static void ViewOnConsole(string title, string uri, DateTime when)
    {
        Console.WriteLine("{0} ({1}): {2}", title, uri, when);
    }

    static void Main(string[] args)
    {
        DowjonesEventWrapper news = new DowjonesEventWrapper();
        news.AddHandler(new DowjonesEventHandler(ViewOnConsole));
        news.AddHandler( (t, uri, dt) => MessageBox.Show(String.Format("{0} ({1}): {2}", t, uri, dt)));
        news.Pull();
        news.RemoveHandler(new DowjonesEventHandler(ViewOnConsole));
        news.Pull();
    }
}
    
