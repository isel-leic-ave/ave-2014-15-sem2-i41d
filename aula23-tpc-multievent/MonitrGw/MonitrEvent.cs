using MonitrGw.Handlers;
using MonitrGw.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitrGw
{
    public class MonitrEvent
    {
        private readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public event LastNewsEventHandler LastNewsEvent;
        public event StockCompetitorsEventHandler StockCompetitorsEvent; 
        public event StockAnalysisEventHandler StockAnalysisEvent;

        public void OnLastNewsEvent() {
            MonitrMarketData data = MonitrApi.GetLastNews();
            var tmp = LastNewsEvent;
            if (tmp != null) 
            {
                DateTime date = start.AddMilliseconds(data.Time).ToLocalTime();
                tmp(data.Title, data.Link, date);
            }
        }

        public void OnStockCompetitorsEvent(string stockSymbol)
        {
            List<string> data = MonitrApi.GetStockCompetitors(stockSymbol);
            var tmp = StockCompetitorsEvent;
            if (tmp != null) tmp(stockSymbol, data);
        }

        public void OnStockAnalysisEvent(string stockSymbol)
        {
            MonitrAnalysisData data = MonitrApi.GetStockAnalysis(stockSymbol);
            var tmp = StockAnalysisEvent;
            if (tmp != null) tmp(data);
        }
    }
}
