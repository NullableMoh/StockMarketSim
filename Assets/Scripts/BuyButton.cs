using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RvveSplit
{
    public class BuyButton : MonoBehaviour
    {
        public delegate void BuyButtonClickedEventHandler(object sender, BuyButtonClickedEventArgs e);
        public BuyButtonClickedEventHandler OnBuyButtonClicked;

        public void InvokeOnBuyButtonClicked()
        {
            OnBuyButtonClicked?.Invoke(this, new BuyButtonClickedEventArgs(this));
        }
    }
}