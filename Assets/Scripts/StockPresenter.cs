using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RvveSplit
{
    public class StockPresenter : MonoBehaviour
    {
        [SerializeField] bool disableStockHoldingsText = false;

        string stockPriceText, stockHoldingsText;

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
        }

    }
}