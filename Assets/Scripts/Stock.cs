using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Stock : MonoBehaviour
{
	[SerializeField] string stockName;
	//all values should logically be positive, but allow some negative values for creepy effect.
	[SerializeField] float initialStockPrice;
	[SerializeField] float minStockPrice, maxStockPrice;
	[SerializeField] SlopeSignTendency slopeSignTendency;
	[SerializeField] [Range(0f,Mathf.Infinity)] float stockPriceUpdateInterval = 0.5f;
	[SerializeField] [Range(0f,Mathf.Infinity)] float slopeSignUpdateInterval = 2f;
	
	[SerializeField] bool randomVolatility;
	[Range(0.001f,0.999f)] [Tooltip("Overriden by randomVolatility bool")][SerializeField] float volatility;
	
	float currentStockPrice, slopeSign, stockHoldings;
	
	public event Action<string, float, float> OnStockPriceUpdated;
	
	void Awake()
	{
		currentStockPrice = initialStockPrice;
		stockHoldings = 0f;

		if(randomVolatility)
			volatility = Random.Range(0.001f,0.999f);
	}
	
	void Start()
	{
		StartCoroutine(UpdateStockPrice());
		StartCoroutine(UpdateSlopeSign());
	}
	
	IEnumerator UpdateStockPrice()
	{
		while(true)
		{
			currentStockPrice = Mathf.Clamp(slopeSign * volatility * (maxStockPrice - minStockPrice) 
				+ currentStockPrice, minStockPrice, maxStockPrice);			

			OnStockPriceUpdated?.Invoke(stockName, currentStockPrice, stockHoldings);

			yield return new WaitForSeconds(stockPriceUpdateInterval);
		}
	}
	
	IEnumerator UpdateSlopeSign()
	{
		while(true)
		{
			int randomSlopeSignRaw = 0;
			if(slopeSignTendency == SlopeSignTendency.Positive)
			{
				randomSlopeSignRaw = Random.Range(-1,5);
			}
			else if(slopeSignTendency == SlopeSignTendency.Negative)
			{
				randomSlopeSignRaw = Random.Range(-4,2);
			}
			else if(slopeSignTendency == SlopeSignTendency.Neutral)
			{
				randomSlopeSignRaw = Random.Range(-1,2);				
			}
			
			slopeSign = Mathf.Sign(randomSlopeSignRaw);
			
			yield return new WaitForSeconds(slopeSignUpdateInterval);
		}
	}
}
