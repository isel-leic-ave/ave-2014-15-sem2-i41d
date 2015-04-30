using MonitrGw.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitrGw
{
    public class MonitrApi
    {
        private const string MONITR_API_KEY = "1e3f8640-f754-11e3-97e9-179fff8a3cc5";
        private static readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly RestClient client = new RestClient("http://api.monitr.com/api");

        public static MonitrMarketData GetLastNews()
        {
            string uri = String.Format("/v1/market/news?apikey={0}&max=1", MONITR_API_KEY);
            RestRequest request = new RestRequest(uri, Method.GET);
            MonitrMarket resp = client.Execute<MonitrMarket>(request).Data;
            if (resp.Data.Count == 0) throw new InvalidOperationException("Empty response from Monitr!");
            return resp.Data[0];
        }

        public static List<string> GetStockCompetitors(string stockSymbol)
        {
            string uri = String.Format("/v1/competitors?symbol={1}&apikey={0}", MONITR_API_KEY, stockSymbol);
            RestRequest request = new RestRequest(uri, Method.GET);
            MonitrCompetitors resp = client.Execute<MonitrCompetitors>(request).Data;
            return resp.Competitors;
        }
        public static MonitrAnalysisData GetStockAnalysis(string stockSymbol)
        {
            string uri = String.Format("/v2/symbol/mentions?symbol={1}&startDay=0&endDay=1&apikey={0}", MONITR_API_KEY, stockSymbol);
            RestRequest request = new RestRequest(uri, Method.GET);
            MonitrAnalysisData resp = client.Execute<MonitrAnalysis>(request).Data.Analysis[0];
            resp.StockSymbol = stockSymbol;
            return resp;
        }
    }
}
