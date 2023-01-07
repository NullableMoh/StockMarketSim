using System;

namespace RvveSplit
{
    public class CompetitorShouldDieEventArgs : EventArgs
    {
        public StockPrice StockToDie { get; private set; }
        public CompetitorShouldDieEventArgs(StockPrice stockToDie)
        {
            StockToDie = stockToDie;
        }
    }
}