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
	MarketOpenerAndCloser[] marketOpenersAndClosers;
	
	public event Action<float> OnCashHoldingsUpdated;
	
	void OnEnable()
	{
		FindMiddleMen();

        marketOpenersAndClosers = FindObjectsOfType<MarketOpenerAndCloser>();
		foreach(var market in marketOpenersAndClosers)
		{
			market.OnMarketOpened += FindMiddleMen;
		}
	}

	void OnDisable()
	{
		foreach(var man in middleMen)
		{
			man.OnCanBuy -= DecreaseCashHoldings;
			man.OnCanSell -= IncreaseCashHoldings;
		}

        foreach (var market in marketOpenersAndClosers)
        {
            market.OnMarketOpened -= FindMiddleMen;
        }
    }

    void FindMiddleMen()
    {
        middleMen = FindObjectsOfType<StockPricePlayerCashMiddleMan>();

        //prevents event from invoking method multiple times;
        foreach (var man in middleMen)
        {
            man.OnCanBuy -= DecreaseCashHoldings;
            man.OnCanSell -= IncreaseCashHoldings;
        }


        foreach (var man in middleMen)
        {
            man.OnCanBuy += DecreaseCashHoldings;
            man.OnCanSell += IncreaseCashHoldings;
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
        currentCashHoldings = Mathf.Clamp(newValue, 0f, Mathf.Infinity);
		OnCashHoldingsUpdated?.Invoke(currentCashHoldings);
	}
}
