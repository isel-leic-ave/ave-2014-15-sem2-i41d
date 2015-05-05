namespace MonitrGw.Model
{
    public class MonitrAnalysisData
    {
        public string StockSymbol { get; set; }
        public int Mentions{get; set; }
        public int TotalSentiment{get; set; }
        public int AverageSentiment{get; set; }
        public int Positive{get; set; }
        public int Negative { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] Mentions: {1}, TotalSentiment: {2}, AverageSentiment: {3}", 
                StockSymbol,
                Mentions, 
                TotalSentiment,
                AverageSentiment);
        }
    }
}
