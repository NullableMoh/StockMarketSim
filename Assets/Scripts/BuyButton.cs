using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuyButton : MonoBehaviour
{
	public event Action OnBuyButtonClicked;

	public void InvokeOnBuyButtonClicked()
	{
		OnBuyButtonClicked?.Invoke();
	}
}
