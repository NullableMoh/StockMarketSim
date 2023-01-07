using System;

namespace RvveSplit
{
    public class CompetitorShouldNotDieEventArgs : EventArgs
    {
        public StockPrice StockToDie { get; private set; }
        public CompetitorShouldNotDieEventArgs(StockPrice stockToDie)
        {
            StockToDie = stockToDie;
        }
    }
}