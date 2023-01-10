using RvveSplit.Stock;
using System;
using System.Collections.Generic;

namespace RvveSplit.Competitors
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