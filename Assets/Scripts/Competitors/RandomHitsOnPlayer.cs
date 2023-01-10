using RvveSplit.CurrentMarket;
using RvveSplit.Stock;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RvveSplit.Competitors
{
    public class RandomHitsOnPlayer : MonoBehaviour
    {
        [SerializeField] int numMarketOpensTillStart;
        [SerializeField] float randomHitInterval = 20f;
        [SerializeField][Range(0.001f, 9.999f)] float playerDeathProbability;

        List<StockPrice> competitorStocks;
        int numMarketOpens;

        MarketOpenerAndCloser marketOpenerAndCloser;
        CompetitorHandler competitorHandler;

        public delegate void RandomHitEventHandler(object sender, RandomHitAttemptedEventArgs e);
        public event RandomHitEventHandler OnRandomHit;


        void OnEnable()
        {
            marketOpenerAndCloser.OnMarketOpened += IncreaseNumMarketOpens;
            competitorHandler.OnCompetitorStocksUpdated += UpdateCompetitorStocks;
        }

        void OnDisable()
        {
            marketOpenerAndCloser.OnMarketOpened -= IncreaseNumMarketOpens;
            competitorHandler.OnCompetitorStocksUpdated -= UpdateCompetitorStocks;
        }

        void IncreaseNumMarketOpens()
        {
            numMarketOpens++;

            if(numMarketOpens == numMarketOpensTillStart)
                StartCoroutine(StartRandomHits());
        }

        IEnumerator StartRandomHits()
        {
            while(true)
            {
                OnRandomHit?.Invoke(this, new RandomHitAttemptedEventArgs(Random.Range(0, 101) <= playerDeathProbability * 100,
                    competitorStocks[Random.Range(0, competitorStocks.Count)].OwnerName));

                yield return new WaitForSeconds(randomHitInterval);
            }
        }

        private void UpdateCompetitorStocks(object sender, CompetitorStocksUpdatedEventArgs e)
        {
            competitorStocks = e.CompetitorStocks;
        }

    }
}
