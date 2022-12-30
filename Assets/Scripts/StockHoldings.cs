﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StockHoldings : MonoBehaviour
{
	float stockHoldings;
	int stockShares;
	public float CurrentStockHoldings{get{return stockHoldings;} private set {stockHoldings = value;}}

	StockPricePlayerCashMiddleMan midMan;
	StockPrice stockPrice;
	
	public event Action<float> OnStockHoldingsUpdated;
	
	void OnEnable()
	{
		midMan = GetComponentInChildren<StockPricePlayerCashMiddleMan>();
		midMan.OnCanBuy += IncreaseStockHoldings;
		midMan.OnCanSell += DecreaseStockHoldings;
	
		stockPrice = GetComponent<StockPrice>();
		stockPrice.OnStockPriceUpdated += UpdateStockHoldings;
	}
	
	void OnDisable()
	{
		midMan.OnCanBuy -= IncreaseStockHoldings;
		midMan.OnCanSell -= DecreaseStockHoldings;
		
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
		stockHoldings = amount;
		OnStockHoldingsUpdated?.Invoke(stockHoldings);
		Debug.Log("holdings updated");
	}
}
