using RvveSplit.Stock;
using System;

namespace RvveSplit.BuyAndSell
{
    public class BlackMarketItemPurchasedEventArgs : EventArgs
    {
        public StockPrice StockPrice { get; private set; }
        public BuyButton BuyButton { get; private set; }
        public BlackMarketItemPurchasedEventArgs(StockPrice stockPrice, BuyButton buyButton)
        {
            StockPrice = stockPrice;
            BuyButton = buyButton;
        }
    }
}