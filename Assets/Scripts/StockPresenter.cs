using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StockPresenter : MonoBehaviour
{
	TextMeshProUGUI text;
	Stock stock;
	
	void OnEnable()
	{
		stock = GetComponent<Stock>();
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
