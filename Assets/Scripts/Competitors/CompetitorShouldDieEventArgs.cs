using RvveSplit.Stock;
using System;
using System.Collections.Generic;

namespace RvveSplit.Competitors
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