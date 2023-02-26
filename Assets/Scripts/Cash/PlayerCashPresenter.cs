using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RvveSplit.Cash
{
    public class PlayerCashPresenter : MonoBehaviour
    {
        float previousCashHoldings;
        Color cashColor;

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

        void UpdateCashHoldingsText(float currentCashHoldings)
        {
            cashColor = currentCashHoldings > previousCashHoldings ? Color.green : currentCashHoldings < previousCashHoldings ? Color.red : cashColor;
            previousCashHoldings = currentCashHoldings;

            text.color = cashColor;
            text.text = $"CASH HOLDINGS: ${currentCashHoldings:0.00}";
        }
    }
}