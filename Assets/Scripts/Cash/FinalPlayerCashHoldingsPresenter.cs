using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RvveSplit.Cash
{
    public class FinalPlayerCashHoldingsPresenter : MonoBehaviour
    {

        TextMeshProUGUI text;

        FinalPlayerCashHoldingsLoader loader;

        private void OnEnable()
        {
            text = GetComponent<TextMeshProUGUI>();
            
            loader = FindObjectOfType<FinalPlayerCashHoldingsLoader>();
            loader.FinalCashHoldingsLoaded += UpdateFinalPlayerCashHoldings;
        }

        private void OnDisable()
        {
            loader.FinalCashHoldingsLoaded -= UpdateFinalPlayerCashHoldings;
        }

        void UpdateFinalPlayerCashHoldings(float finalPlayerCashHoldings)
        {
            text.text = $"Final Player Cash Holdings: ${finalPlayerCashHoldings:0.00}";
        }
    }
}
