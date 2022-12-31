using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class StockPrice : MonoBehaviour
{
	[SerializeField] string stockName;
	//all values should logically be positive, but allow some negative values for creepy effect.
	[SerializeField] float initialStockPrice;
	[SerializeField] float minStockPrice, maxStockPrice;
	[SerializeField] SlopeSignTendency slopeSignTendency;
	[SerializeField] [Range(0.001f,9999f)] float stockPriceUpdateInterval = 0.5f;
	[SerializeField] [Range(0.001f,9999f)] float slopeSignUpdateInterval = 2f;

	[SerializeField] bool randomVolatility;
	[Range(0.001f,0.999f)] [Tooltip("Overriden by randomVolatility bool")][SerializeField] float volatility;

	[SerializeField] bool isBlackMarketItem = false;
	//[SerializeField] [Tooltip("Cannot have owner if isBlackMarketItem == true")] string ownerName;

	float currentStockPrice, slopeSign;
	//bool hasOwner;
	public float CurrentStockPrice {get{return currentStockPrice;} private set { currentStockPrice = value;} }
	public bool IsBlackMarketItem { get { return isBlackMarketItem; } }

	public event Action<string, float> OnStockPriceUpdated;

	void Awake()
	{
		currentStockPrice = initialStockPrice;

		if(randomVolatility)
			volatility = Random.Range(0.001f,0.999f);

		//hasOwner = !isBlackMarketItem;
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

			OnStockPriceUpdated?.Invoke(stockName, currentStockPrice);

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
