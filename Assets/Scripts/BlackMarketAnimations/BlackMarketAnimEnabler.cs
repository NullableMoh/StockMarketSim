using RvveSplit.BuyAndSell;
using RvveSplit.Stock;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RvveSplit.BlackMarketAnimations
{
    public class BlackMarketAnimEnabler : MonoBehaviour
    {
        [SerializeField] GameObject animObject;

        StockPricePlayerCashMiddleMan[] middleMen;
        StockPrice parentStock;

        private void OnEnable()
        {
            parentStock = GetComponentInParent<StockPrice>();

            middleMen = FindObjectsOfType<StockPricePlayerCashMiddleMan>();
            foreach (var man in middleMen)
            {
                man.OnBlackMarketItemPurchased += CheckIfBlackMarketIsParent;
            }
        }

        private void OnDisable()
        {
            foreach (var man in middleMen)
            {
                man.OnBlackMarketItemPurchased -= CheckIfBlackMarketIsParent;
            }
        }

        private void CheckIfBlackMarketIsParent(object sender, BlackMarketItemPurchasedEventArgs e)
        {
            if (parentStock == e.StockPrice)
            {
                animObject.SetActive(true);
            }
        }
    }
}