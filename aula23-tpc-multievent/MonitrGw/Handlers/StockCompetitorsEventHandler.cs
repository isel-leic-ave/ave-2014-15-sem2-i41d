using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitrGw.Handlers
{
    public delegate void StockCompetitorsEventHandler(string stockSymbol, List<String> competitors);
}
