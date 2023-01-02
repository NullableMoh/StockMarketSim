using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class CompetitorHandler : MonoBehaviour
{
    [SerializeField] List<StockPrice> competitorStocks;

    BuyButton[] buyButtons;
    MarketOpenerAndCloser[] markets;

    public delegate void CompetitorShouldDieEventHandler(object sender, CompetitorShouldDieEventArgs e);
    public event CompetitorShouldDieEventHandler OnCompetitorShouldDie;

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
            int deathRoll = Random.Range(0, (100 - (int)competitor.OwnerDeathProbability * 100) + 1);
            
            if(deathRoll == 0 && competitor.OwnerDeathProbability > 0 && !competitor.IsBlackMarketItem)
            {
                competitorStocks.Remove(competitor);
                OnCompetitorShouldDie?.Invoke(this, new CompetitorShouldDieEventArgs(competitor));
            }
        }
    }
}
