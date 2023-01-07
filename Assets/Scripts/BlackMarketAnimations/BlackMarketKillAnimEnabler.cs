using RvveSplit.BuyAndSell;
using RvveSplit.Competitors;
using RvveSplit.Stock;
using System;
using UnityEngine;

namespace RvveSplit.BlackMarketAnimations
{
    public class BlackMarketKillAnimEnabler : MonoBehaviour
    {
        [SerializeField] GameObject killSuccessfulAnimObj, killFailedAnimObj;

        bool blackMarketItemIsParent;
        StockPricePlayerCashMiddleMan[] middleMen;
        StockPrice parentStock;

        CompetitorHandler competitorHandler;

        private void Awake()
        {
            blackMarketItemIsParent = false;
        }

        private void OnEnable()
        {
            parentStock = GetComponentInParent<StockPrice>();

            middleMen = FindObjectsOfType<StockPricePlayerCashMiddleMan>();
            foreach (var man in middleMen)
            {
                man.OnBlackMarketItemPurchased += CheckIfBlackMarketIsParent;
            }

            competitorHandler = FindObjectOfType<CompetitorHandler>();
            competitorHandler.OnCompetitorShouldDie += PlayKillSuccessfulAnim;
            competitorHandler.OnCompetitorShouldNotDie += PlayKillFailedAnim;
        }

        private void OnDisable()
        {
            foreach (var man in middleMen)
            {
                man.OnBlackMarketItemPurchased -= CheckIfBlackMarketIsParent;
            }


            competitorHandler.OnCompetitorShouldDie -= PlayKillSuccessfulAnim;
            competitorHandler.OnCompetitorShouldNotDie -= PlayKillFailedAnim;
        }

        private void CheckIfBlackMarketIsParent(StockPrice stock)
        {
            blackMarketItemIsParent = parentStock == stock;
        }

        private void PlayKillSuccessfulAnim(object sender, CompetitorShouldDieEventArgs e)
        {
            if (!blackMarketItemIsParent) return;

            killSuccessfulAnimObj.SetActive(true);
        }
        private void PlayKillFailedAnim(object sender, CompetitorShouldNotDieEventArgs e)
        {
            if (!blackMarketItemIsParent) return;

            killFailedAnimObj.SetActive(true);
        }
    }
}