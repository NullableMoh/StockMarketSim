using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StockPresenter : MonoBehaviour
{
	TextMeshProUGUI text;
	StockPrice stock;
	
	void OnEnable()
	{
		stock = GetComponent<StockPrice>();
		stock.OnStockPriceUpdated += UpdateStockUI;		
		
		text = GetComponent<TextMeshProUGUI>();
	}
	
	void OnDisable()
	{
		stock.OnStockPriceUpdated -= UpdateStockUI;		
	}
	
	void UpdateStockUI(string stockName, float currentStockPrice, float stockHoldings)
	{
		text.text = $"{stockName}: ${currentStockPrice:0.00}\n" +
					$"HOLDINGS: ${stockHoldings:0.00}";
	}

	
}
