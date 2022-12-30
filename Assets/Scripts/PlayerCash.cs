using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
	[SerializeField] float startingCashHoldings;
	
	float currentCashHoldings;
	
	public float CurrentCashHoldings {get{return currentCashHoldings;}}
	
	void Awake()
	{
		currentCashHoldings = startingCashHoldings;
	}
}
