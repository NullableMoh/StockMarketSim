using System;

public class CompetitorShouldNotDieEventArgs : EventArgs
{
    public StockPrice StockToDie { get; private set; }
    public CompetitorShouldNotDieEventArgs(StockPrice stockToDie)
    {
        StockToDie = stockToDie;
    }
}