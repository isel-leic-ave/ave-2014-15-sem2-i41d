using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitrGw.Model
{
    public class MonitrAnalysis
    {
        public int Status { get; set; }
        public List<MonitrAnalysisData> Analysis { get; set; }
    }
}
