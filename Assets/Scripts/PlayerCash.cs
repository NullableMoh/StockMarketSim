using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerCash : MonoBehaviour
{
	[SerializeField] float startingCashHoldings;
	
	public float CurrentCashHoldings {get{return currentCashHoldings;} private set {currentCashHoldings = value;}}
	float currentCashHoldings;

	StockPricePlayerCashMiddleMan[] middleMen;
	
	public event Action<float> OnCashHoldingsUpdated;
	
	void OnEnable()
	{
		middleMen = FindObjectsOfType<StockPricePlayerCashMiddleMan>();
		foreach(var man in middleMen)
		{
			man.OnCanBuy += DecreaseCashHoldings;
			man.OnCanSell += IncreaseCashHoldings;
		}
	}
	
	void OnDisable()
	{
		foreach(var man in middleMen)
		{
			man.OnCanBuy -= DecreaseCashHoldings;
			man.OnCanSell -= IncreaseCashHoldings;
		}	
	}

	void Start()
	{
		SetCashHoldings(startingCashHoldings);
	}
	
	void DecreaseCashHoldings(float amount)
	{
		SetCashHoldings(currentCashHoldings - amount);
	}
	
	void IncreaseCashHoldings(float amount)
	{
		SetCashHoldings(currentCashHoldings + amount);
	}
	
	void SetCashHoldings(float newValue)
	{
		currentCashHoldings = newValue;
		OnCashHoldingsUpdated?.Invoke(currentCashHoldings);
	}
}
