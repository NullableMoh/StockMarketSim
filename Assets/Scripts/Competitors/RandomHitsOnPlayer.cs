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
        [SerializeField][Range(0.001f, 0.999f)] float playerDeathProbability;

        List<StockPrice> competitorStocks;
        int numMarketOpens;
        bool hitsEnabled = true;

        MarketOpenerAndCloser[] marketOpenersAndClosers;
        CompetitorHandler competitorHandler;

        public delegate void RandomHitEventHandler(object sender, RandomHitAttemptedEventArgs e);
        public event RandomHitEventHandler OnRandomHit;


        void OnEnable()
        {
            marketOpenersAndClosers = FindObjectsOfType<MarketOpenerAndCloser>();
            competitorHandler= FindObjectOfType<CompetitorHandler>();

            
            foreach(var openerAndCloser in marketOpenersAndClosers) openerAndCloser.OnMarketOpened += IncreaseNumMarketOpens;

            competitorHandler.OnCompetitorStocksUpdated += UpdateCompetitorStocks;
            competitorHandler.ASSNStockPlaying += DisableHits;
            competitorHandler.ASSNStockStoppedPlaying += EnableHits;
        }

        void OnDisable()
        {
            foreach(var openerAndCloser in marketOpenersAndClosers) openerAndCloser.OnMarketOpened -= IncreaseNumMarketOpens;

            competitorHandler.OnCompetitorStocksUpdated -= UpdateCompetitorStocks;
            competitorHandler.ASSNStockPlaying -= DisableHits;
            competitorHandler.ASSNStockStoppedPlaying -= EnableHits;
        }

        void IncreaseNumMarketOpens()
        {
            numMarketOpens++;

            if(numMarketOpens == numMarketOpensTillStart)
            {
                StartCoroutine(StartRandomHits());
            }
        }


        IEnumerator StartRandomHits()
        {
            while(true)
            {
                if(competitorStocks.Count <= 0) break;

                if (hitsEnabled)
                {
                    OnRandomHit?.Invoke(this, new RandomHitAttemptedEventArgs(Random.Range(0, 101) <= playerDeathProbability * 100,
                        competitorStocks[Random.Range(0, competitorStocks.Count)].OwnerName));

                    yield return new WaitForSeconds(randomHitInterval);
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        private void UpdateCompetitorStocks(object sender, CompetitorStocksUpdatedEventArgs e)
        {
            competitorStocks = e.CompetitorStocks;
        }

        void DisableHits()
        {
            hitsEnabled = false;
        }

        void EnableHits()
        {
            hitsEnabled = true;
        }
    }
}
