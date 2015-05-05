using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitrGw.Model
{
    class MonitrMarket
    {
        public int Status { get; set; }
        public List<MonitrMarketData> Data { get; set; }
    }

}
