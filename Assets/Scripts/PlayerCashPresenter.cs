using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCashPresenter : MonoBehaviour
{
	TextMeshProUGUI text;
	PlayerCash cash;
	
	void OnEnable()
	{
		text = GetComponent<TextMeshProUGUI>();
		
		cash = FindObjectOfType<PlayerCash>();
		cash.OnCashHoldingsUpdated += UpdateCashHoldingsText;
	}
	
	void OnDisable()
	{
		cash.OnCashHoldingsUpdated -= UpdateCashHoldingsText;
	}

	void UpdateCashHoldingsText(float amount)
	{
		text.text =  $"CASH HOLDINGS: ${amount}";		
	}
}
