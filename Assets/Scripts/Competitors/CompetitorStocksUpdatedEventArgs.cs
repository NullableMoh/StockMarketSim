#nullable enable
using RvveSplit.Stock;
using System;
using System.Collections.Generic;

namespace RvveSplit.Competitors
{
    public class CompetitorStocksUpdatedEventArgs : EventArgs
    {
        public List<StockPrice>? CompetitorStocks { get; private set; }
        public CompetitorStocksUpdatedEventArgs(List<StockPrice> competitorStocks)
        {
            CompetitorStocks = competitorStocks;
        }
    }
}