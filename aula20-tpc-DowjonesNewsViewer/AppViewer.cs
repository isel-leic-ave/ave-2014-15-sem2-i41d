using System;
using System.Windows.Forms;
    
class AppViewer
{
    class ConsoleViewer : Subscriber
    {
        public void Occurrence(string title, string uri, DateTime when)
        {
            Console.WriteLine("{0} ({1}): {2}", title, uri, when);
        }
    }

    class MboxAlerter : Subscriber
    {
        public void Occurrence(string title, string uri, DateTime when)
        {
            MessageBox.Show(String.Format("{0} ({1}): {2}", title, uri, when));
        }
    }

    static void Main(string[] args)
    {
        DowjonesNews news = new DowjonesNews();
        news.AddSubscriber(new ConsoleViewer());
        news.AddSubscriber(new MboxAlerter());
        news.Pull();
    }
}
    
