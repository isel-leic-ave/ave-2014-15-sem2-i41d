using MonitrGw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace MonitrApp
{
    class Program
    {
        public static void MarketLastNews(string title, string uri, DateTime when)
        {
            Console.WriteLine("{0} ({1}): {2}", title, uri, when);
        }

        public static void StockCompetitors(string stockSymbol, List<String> competitors)
        {
            Console.WriteLine();
            Console.WriteLine("############ {0} Competitors ############", stockSymbol);
            competitors.ForEach(Console.WriteLine);
        }

        static void Main(string[] args)
        {
            MonitrEvent news = new MonitrEvent();
            news.LastNewsEvent += MarketLastNews;
            news.LastNewsEvent += MarketLastNews;
            news.StockCompetitorsEvent += StockCompetitors;
            news.StockAnalysisEvent += 
                analysisData => MessageBox.Show(analysisData.ToString(), analysisData.StockSymbol);

            
            news.OnLastNewsEvent();
            news.OnStockCompetitorsEvent("AAPL");
            news.OnStockAnalysisEvent("GOOG");
        }
    }
}
