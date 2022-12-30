using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StockPricePlayerCashMiddleMan : MonoBehaviour
{
	BuyButton buyButton;
	SellButton sellButton;
	StockPrice stockPrice;
	StockHoldings stockHoldings;
	PlayerCash playerCash;
	
	public event Action<float> OnCanBuy;
	public event Action<float> OnCanSell;
	
	void OnEnable()
	{
		buyButton = GetComponentInChildren<BuyButton>();
		buyButton.OnBuyButtonClicked += CheckIfCanBuy;
		
		sellButton = GetComponentInChildren<SellButton>();
		sellButton.OnSellButtonClicked += CheckIfCanSell;
	}
	
	void OnDisable()
	{
		buyButton.OnBuyButtonClicked -= CheckIfCanBuy;
		sellButton.OnSellButtonClicked -= CheckIfCanSell;
	}

	void Start()
	{
		stockPrice = GetComponent<StockPrice>();
		stockHoldings = GetComponent<StockHoldings>();
		playerCash = FindObjectOfType<PlayerCash>();
	}
    
	void CheckIfCanBuy()
	{
		if(stockPrice.CurrentStockPrice <= playerCash.CurrentCashHoldings)
			OnCanBuy?.Invoke(stockPrice.CurrentStockPrice);
	}
	
	void CheckIfCanSell()
	{
		if(stockHoldings.CurrentStockHoldings >= stockPrice.CurrentStockPrice)
			OnCanSell?.Invoke(stockPrice.CurrentStockPrice);
	}
}