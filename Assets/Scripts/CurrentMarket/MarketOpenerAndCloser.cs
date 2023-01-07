using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.CurrentMarket
{
    public class MarketOpenerAndCloser : MonoBehaviour
    {
        [SerializeField] int openTime = 300;
        [SerializeField] int closeTime = 1260;
        [SerializeField] GameObject market;

        MarketClock clock;

        public event Action OnMarketOpened;

        private void OnEnable()
        {
            clock = FindObjectOfType<MarketClock>();
            clock.OnTimeUpdated += CheckIfShouldEnableOrDisableMarket;
        }

        private void OnDisable()
        {
            clock.OnTimeUpdated -= CheckIfShouldEnableOrDisableMarket;
        }

        private void CheckIfShouldEnableOrDisableMarket(int time)
        {
            if (time == openTime)
            {
                market.SetActive(true);
                OnMarketOpened?.Invoke();
            }

            else if (time == closeTime)
                market.SetActive(false);
        }
    }
}