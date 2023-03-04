using RvveSplit.BlackMarketAnimations;
using RvveSplit.BuyAndSell;
using RvveSplit.CurrentMarket;
using RvveSplit.Stock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RvveSplit.Competitors
{

    public class CompetitorHandler : MonoBehaviour
    {
        [SerializeField] List<StockPrice> competitorStocks;
        [SerializeField] float AssasinationAttemptCooldown = 5f;

        float timeAtASSNBuy;
        StockPricePlayerCashMiddleMan[] middleMen;
        MarketOpenerAndCloser[] markets;

        public delegate void CompetitorShouldDieEventHandler(object sender, CompetitorShouldDieEventArgs e);
        public event CompetitorShouldDieEventHandler OnCompetitorShouldDie;

        public delegate void CompetitorShouldNotDieEventHandler(object sender, CompetitorShouldNotDieEventArgs e);
        public event CompetitorShouldNotDieEventHandler OnCompetitorShouldNotDie;

        public delegate void AllCompetitorsKilledEventHandler(object sender, AllCompetitorsKilledEventArgs e);
        public event AllCompetitorsKilledEventHandler OnAllCompetitorsKilled;

        public delegate void CompetitorStocksUpdatedEventHandler(object sender, CompetitorStocksUpdatedEventArgs e);
        public event CompetitorStocksUpdatedEventHandler OnCompetitorStocksUpdated;

        public event Action ASSNStockPlaying;
        public event Action ASSNStockStoppedPlaying;

        private void Awake()
        {
            OnCompetitorStocksUpdated?.Invoke(this, new CompetitorStocksUpdatedEventArgs(competitorStocks));
        }

        private void OnEnable()
        {
            FindBlackMarketItems();

            markets = FindObjectsOfType<MarketOpenerAndCloser>();
            foreach (var market in markets)
                market.OnMarketOpened += FindBlackMarketItems;
        }

        private void OnDisable()
        {
            foreach (var man in middleMen)
                man.OnBlackMarketItemPurchased -= CheckIfOwnedByASSNStock;

            foreach (var market in markets)
                market.OnMarketOpened -= FindBlackMarketItems;
        }
        void FindBlackMarketItems()
        {
            middleMen = FindObjectsOfType<StockPricePlayerCashMiddleMan>();

            foreach (var man in middleMen)
            {
                man.OnBlackMarketItemPurchased -= CheckIfOwnedByASSNStock;
            }

            foreach (var man in middleMen)
            {
                man.OnBlackMarketItemPurchased += CheckIfOwnedByASSNStock;
            }
        }

        private void Update()
        {
            if (Time.time < timeAtASSNBuy + AssasinationAttemptCooldown)
            {
                ASSNStockPlaying?.Invoke();
            }
            else
            {
                ASSNStockStoppedPlaying?.Invoke();
            }
        }

        void CheckIfOwnedByASSNStock(object sender, BlackMarketItemPurchasedEventArgs e)
        {
            if (competitorStocks.Count <= 0) return;
            if (!e.BuyButton.GetComponentInParent<ASSNStock>()) return;
            if (Time.time < timeAtASSNBuy + AssasinationAttemptCooldown) return;


            var competitor = competitorStocks[Random.Range(0, competitorStocks.Count)];
            var chance = Random.Range(0, 101);

            if (chance <= competitor.OwnerDeathProbability * 100 && competitor.OwnerDeathProbability > 0 && !competitor.IsBlackMarketItem)
            {
                competitorStocks.Remove(competitor);
                OnCompetitorShouldDie?.Invoke(this, new CompetitorShouldDieEventArgs(competitor));
                OnCompetitorStocksUpdated?.Invoke(this, new CompetitorStocksUpdatedEventArgs(competitorStocks));
            }
            else
            {
                OnCompetitorShouldNotDie?.Invoke(this, new CompetitorShouldNotDieEventArgs(competitor));
            }

            if (competitorStocks.Count == 0)
            {
                OnAllCompetitorsKilled?.Invoke(this, new AllCompetitorsKilledEventArgs());
            }

            timeAtASSNBuy = Time.time;
        }
    }
}