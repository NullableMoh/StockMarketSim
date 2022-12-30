using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuyButton : MonoBehaviour
{
	public event Action<float> OnBuyButtonClicked;
	
	float stockPrice;
	
	Stock stock;
	void OnEnable()
	{
		stock = GetComponentInParent<Stock>();
		stock.OnStockPriceUpdated += UpdateStockPrice;
	}
	
	// This function is called when the behaviour becomes disabled () or inactive.
	void OnDisable()
	{
		stock.OnStockPriceUpdated -= UpdateStockPrice;
	}
	
	// Awake is called when the script instance is being loaded.
	void Awake()
	{
		stockPrice = 0;	
	}
	
	public void InvokeOnBuyButtonClicked()
	{
		OnBuyButtonClicked?.Invoke(stockPrice);
	}
	
	void UpdateStockPrice(string _, float currentStockPrice, float __)
	{
		stockPrice = currentStockPrice;
	}
}
