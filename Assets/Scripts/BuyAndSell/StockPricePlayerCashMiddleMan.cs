using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RvveSplit.Stock;
using RvveSplit.Cash;

namespace RvveSplit.BuyAndSell
{
    public class StockPricePlayerCashMiddleMan : MonoBehaviour
    {
        BuyButton buyButton;
        SellButton sellButton;
        StockPrice stockPrice;
        StockHoldings stockHoldings;
        PlayerCash playerCash;

        public event Action<float> CanBuy;
        public event Action<float> CanSell;

        public event Action CannotBuy;
        public event Action CannotSell;

        public delegate void BlackMarketItemPurchasedEventHandler(object sender, BlackMarketItemPurchasedEventArgs e);
        public event BlackMarketItemPurchasedEventHandler OnBlackMarketItemPurchased;

        void OnEnable()
        {
            stockPrice = GetComponent<StockPrice>();
            stockHoldings = GetComponent<StockHoldings>();
            playerCash = FindObjectOfType<PlayerCash>();


            buyButton = GetComponentInChildren<BuyButton>();
            buyButton.OnBuyButtonClicked += CheckIfCanBuy;

            sellButton = GetComponentInChildren<SellButton>();

            if (sellButton)
                sellButton.OnSellButtonClicked += CheckIfCanSell;
        }

        void OnDisable()
        {
            buyButton.OnBuyButtonClicked -= CheckIfCanBuy;

            if (sellButton)
                sellButton.OnSellButtonClicked -= CheckIfCanSell;
        }

        void CheckIfCanBuy(object sender, BuyButtonClickedEventArgs e)
        {
            if (playerCash.CurrentCashHoldings >= stockPrice.CurrentStockPrice)
            {
                CanBuy?.Invoke(stockPrice.CurrentStockPrice);
                if (stockPrice.IsBlackMarketItem)
                {
                    OnBlackMarketItemPurchased?.Invoke(this, new BlackMarketItemPurchasedEventArgs(stockPrice, buyButton));
                }
            }
            else
            {
                CannotBuy?.Invoke();
            }
        }

        void CheckIfCanSell()
        {
            if (stockHoldings.CurrentStockHoldings >= stockPrice.CurrentStockPrice)
            {
                CanSell?.Invoke(stockPrice.CurrentStockPrice);
            }
            else
            {
                CannotSell.Invoke();
            }
        }
    }
}