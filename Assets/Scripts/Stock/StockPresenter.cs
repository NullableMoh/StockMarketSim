using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RvveSplit.Stock
{
    public class StockPresenter : MonoBehaviour
    {
        [SerializeField] bool disableStockHoldingsText = false;

        string stockPriceText, stockHoldingsText;
        float previousStockPrice;
        Color stockColor;

        TextMeshProUGUI text;
        StockPrice stockPrice;
        StockHoldings stockHoldings;

        void OnEnable()
        {
            stockPrice = GetComponent<StockPrice>();
            stockPrice.OnStockPriceUpdated += UpdateStockPriceText;

            stockHoldings = GetComponent<StockHoldings>();
            stockHoldings.OnStockHoldingsUpdated += UpdateStockHoldingsText;

            text = GetComponent<TextMeshProUGUI>();
        }

        void OnDisable()
        {
            stockPrice.OnStockPriceUpdated -= UpdateStockPriceText;
            stockHoldings.OnStockHoldingsUpdated -= UpdateStockHoldingsText;
        }

        void UpdateStockPriceText(string stockName, float currentStockPrice)
        {
            
            stockPriceText = $"{stockName}: ${currentStockPrice:0.00}";

            stockColor = (currentStockPrice >= previousStockPrice) ? Color.green : Color.red;

            previousStockPrice = currentStockPrice;
            UpdateStockUI();
        }

        void UpdateStockHoldingsText(float currentStockHoldings)
        {
            if (disableStockHoldingsText)
            {
                stockHoldingsText = "";
            }
            else
            {
                stockHoldingsText = $"HOLDINGS: ${currentStockHoldings:0.00}";
            }

            UpdateStockUI();
        }

        void UpdateStockUI()
        {
            text.text = stockPriceText + "\n" + stockHoldingsText;
            text.color = stockColor;
        }

    }
}