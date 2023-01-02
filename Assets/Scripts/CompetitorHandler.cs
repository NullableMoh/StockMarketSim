using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class AllCompetitorsKilledEventArgs : EventArgs
{

}

public class CompetitorHandler : MonoBehaviour
{
    [SerializeField] List<StockPrice> competitorStocks;

    BuyButton[] buyButtons;
    MarketOpenerAndCloser[] markets;

    public delegate void CompetitorShouldDieEventHandler(object sender, CompetitorShouldDieEventArgs e);
    public event CompetitorShouldDieEventHandler OnCompetitorShouldDie;

    public delegate void CompetitorShouldNotDieEventHandler(object sender, CompetitorShouldNotDieEventArgs e);
    public event CompetitorShouldNotDieEventHandler OnCompetitorShouldNotDie;

    public delegate void AllCompetitorsKilledEventHandler(object sender, AllCompetitorsKilledEventArgs e);
    public event AllCompetitorsKilledEventHandler OnAllCompetitorsKilled;

    private void OnEnable()
    {
        FindBuyButtons();
        
        markets = FindObjectsOfType<MarketOpenerAndCloser>();
        foreach (var market in markets)
            market.OnMarketOpened += FindBuyButtons;
    }

    private void OnDisable()
    {
        foreach (var button in buyButtons)
            button.OnBuyButtonClicked -= CheckIfOwnedByASSNStock;

        foreach (var market in markets)
            market.OnMarketOpened -= FindBuyButtons;
    }
    void FindBuyButtons()
    {
        buyButtons = FindObjectsOfType<BuyButton>();

        foreach (var button in buyButtons)
        {
            button.OnBuyButtonClicked -= CheckIfOwnedByASSNStock;
        }
        
        foreach (var button in buyButtons)
        {
            button.OnBuyButtonClicked += CheckIfOwnedByASSNStock;
        }
    }

    void CheckIfOwnedByASSNStock(object sender, BuyButtonClickedEventArgs e)
    {
        if (e.BuyButton.GetComponentInParent<ASSNStock>() && competitorStocks.Count > 0)
        {
            var competitor = competitorStocks[Random.Range(0, competitorStocks.Count)];
            var chance = Random.Range(0, 101);
            
            if(chance <= competitor.OwnerDeathProbability * 100 && competitor.OwnerDeathProbability > 0 && !competitor.IsBlackMarketItem)
            {
                competitorStocks.Remove(competitor);
                OnCompetitorShouldDie?.Invoke(this, new CompetitorShouldDieEventArgs(competitor));
            }
            else
            {
                OnCompetitorShouldNotDie?.Invoke(this, new CompetitorShouldNotDieEventArgs(competitor));
            }

            if(competitorStocks.Count == 0)
                OnAllCompetitorsKilled?.Invoke(this, new AllCompetitorsKilledEventArgs());
        }
    }
}
