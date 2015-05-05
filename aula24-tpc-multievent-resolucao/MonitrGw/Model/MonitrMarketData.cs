using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitrGw.Model
{
    public class MonitrMarketData
    {

        private readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public string Market { get; set; }
        public string Title { get; set; }
        public string StockSymbol { get; set; }
        public string Link { get; set; }
        public long Time { get; set; }
        public string Domain { get; set; }

        public override string ToString()
        {
            DateTime date = start.AddMilliseconds(Time).ToLocalTime();
            return string.Format("{2}: {0} ({1})", Title, Link, date);
        }
    }
}