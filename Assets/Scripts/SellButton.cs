using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RvveSplit
{
    public class SellButton : MonoBehaviour
    {
        public event Action OnSellButtonClicked;

        public void InvokeOnSellButtonClicked()
        {
            OnSellButtonClicked?.Invoke();
        }
    }
}