using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RvveSplit.BuyAndSell;

namespace RvveSplit.Stock
{
    public class StockHoldings : MonoBehaviour
    {
        float stockHoldings;
        int stockShares;
        public float CurrentStockHoldings { get { return stockHoldings; } private set { stockHoldings = value; } }

        StockPricePlayerCashMiddleMan midMan;
        StockPrice stockPrice;

        public event Action<float> OnStockHoldingsUpdated;

        void OnEnable()
        {
            stockPrice = GetComponent<StockPrice>();
            stockPrice.OnStockPriceUpdated += UpdateStockHoldings;

            midMan = GetComponent<StockPricePlayerCashMiddleMan>();
            midMan.CanBuy += IncreaseStockHoldings;
            midMan.CanSell += DecreaseStockHoldings;
        }

        void OnDisable()
        {
            midMan.CanBuy -= IncreaseStockHoldings;
            midMan.CanSell -= DecreaseStockHoldings;

            stockPrice.OnStockPriceUpdated -= UpdateStockHoldings;
        }

        void Awake()
        {
            SetStockHoldings(0f);
            stockShares = 0;
        }

        void UpdateStockHoldings(string _, float currentStockPrice)
        {
            SetStockHoldings(stockShares * currentStockPrice);
        }

        void IncreaseStockHoldings(float amount)
        {
            SetStockHoldings(stockHoldings + amount);
            stockShares++;
        }

        void DecreaseStockHoldings(float amount)
        {
            SetStockHoldings(stockHoldings - amount);
            stockShares--;
        }

        void SetStockHoldings(float amount)
        {
            stockHoldings = Mathf.Clamp(amount, 0f, Mathf.Infinity);
            OnStockHoldingsUpdated?.Invoke(stockHoldings);
        }
    }
}