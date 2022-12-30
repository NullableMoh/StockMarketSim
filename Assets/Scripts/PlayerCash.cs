using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
	[SerializeField] float startingCash;
	
	BuyButton[] buyButtons;
	float currentCash;
	
	void Awake()
	{
		currentCash = startingCash;
	}
	
	void OnEnable()
	{
		buyButtons = FindObjectsOfType<BuyButton>();
		foreach(var buyButton in buyButtons)
			buyButton.OnBuyButtonClicked += CheckIfEnoughCashToBuyStock;
	}
	
	void OnDisable()
	{
		foreach(var buyButton in buyButtons)
			buyButton.OnBuyButtonClicked -= CheckIfEnoughCashToBuyStock;		
	}
	
	void CheckIfEnoughCashToBuyStock(float stockPrice)
	{
		Debug.Log("Recieved Stock Price: " + stockPrice);
	}
}
