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
        private Dictionary<Type, Delegate> eventDic = new Dictionary<Type, Delegate>();

        private void AddHandler(Delegate h)
        {
            Delegate aux;
            if (eventDic.TryGetValue(h.GetType(), out aux))
            {
                aux = Delegate.Combine(aux, h); //aux += h; // !!!1 Erro e compilação nao sabe o tipo destino do cast
                eventDic[h.GetType()] = aux;
            }
            else
                eventDic.Add(h.GetType(), h);
        }
        private void RemoveHandler(Delegate h)
        {
            Delegate aux;
            if (eventDic.TryGetValue(h.GetType(), out aux))
            {
                aux = Delegate.Remove(aux, h);
                eventDic[h.GetType()] = aux;
            }
        }

        public event LastNewsEventHandler LastNewsEvent
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        public event StockCompetitorsEventHandler StockCompetitorsEvent
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        public event StockAnalysisEventHandler StockAnalysisEvent
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }

        public void OnLastNewsEvent()
        {
            MonitrMarketData data = MonitrApi.GetLastNews();
            Delegate aux;
            if (eventDic.TryGetValue(typeof(LastNewsEventHandler), out aux))
            {
                LastNewsEventHandler h = ((LastNewsEventHandler)aux);
                h(data.Title, data.Link, start.AddMilliseconds(data.Time).ToLocalTime());
            }
        }


        public void OnStockCompetitorsEvent(string stockSymbol)
        {
            List<string> data = MonitrApi.GetStockCompetitors(stockSymbol);
            Delegate aux;
            if (eventDic.TryGetValue(typeof(StockCompetitorsEventHandler), out aux))
            {
                StockCompetitorsEventHandler h = ((StockCompetitorsEventHandler)aux);
                h(stockSymbol, data);
            }

        }

        public void OnStockAnalysisEvent(string stockSymbol)
        {
            MonitrAnalysisData data = MonitrApi.GetStockAnalysis(stockSymbol);
            Delegate aux;
            if (eventDic.TryGetValue(typeof(StockAnalysisEventHandler), out aux))
            {
                StockAnalysisEventHandler h = ((StockAnalysisEventHandler)aux);
                h(data);
            }

        }

        private void OnEvent(Type eventType, params Object [] args) { 
            Delegate aux;
            if (eventDic.TryGetValue(eventType, out aux)) {
                aux.DynamicInvoke(args); // Chamada via reflexão
            }
            
        }
    }
}
