using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using RvveSplit.BuyAndSell;
using RvveSplit.CurrentMarket;
using RvveSplit.Competitors;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RvveSplit.Cash
{
    public class PlayerCash : MonoBehaviour
    {
        [SerializeField] float startingCashHoldings;

        public float CurrentCashHoldings { get { return currentCashHoldings; } private set { currentCashHoldings = value; } }
        float currentCashHoldings;

        StockPricePlayerCashMiddleMan[] middleMen;
        MarketOpenerAndCloser[] marketOpenersAndClosers;

        public event Action<float> OnCashHoldingsUpdated;

        GameWonActivator gameWonActivator;

        void OnEnable()
        {
            FindMiddleMen();

            marketOpenersAndClosers = FindObjectsOfType<MarketOpenerAndCloser>();
            foreach (var market in marketOpenersAndClosers)
            {
                market.OnMarketOpened += FindMiddleMen;
            }
            
            gameWonActivator = FindObjectOfType<GameWonActivator>();
            gameWonActivator.GameWon += SavePlayerCash;
        }

        void OnDisable()
        {
            foreach (var man in middleMen)
            {
                man.OnCanBuy -= DecreaseCashHoldings;
                man.OnCanSell -= IncreaseCashHoldings;
            }

            foreach (var market in marketOpenersAndClosers)
            {
                market.OnMarketOpened -= FindMiddleMen;
            }

            gameWonActivator.GameWon -= SavePlayerCash;
        }

        void FindMiddleMen()
        {
            middleMen = FindObjectsOfType<StockPricePlayerCashMiddleMan>();

            //prevents event from invoking method multiple times;
            foreach (var man in middleMen)
            {
                man.OnCanBuy -= DecreaseCashHoldings;
                man.OnCanSell -= IncreaseCashHoldings;
            }


            foreach (var man in middleMen)
            {
                man.OnCanBuy += DecreaseCashHoldings;
                man.OnCanSell += IncreaseCashHoldings;
            }
        }

        void Start()
        {
            SetCashHoldings(startingCashHoldings);
            SavePlayerCash();
        }

        void DecreaseCashHoldings(float amount)
        {
            SetCashHoldings(currentCashHoldings - amount);
        }

        void IncreaseCashHoldings(float amount)
        {
            SetCashHoldings(currentCashHoldings + amount);
        }

        void SetCashHoldings(float newValue)
        {
            currentCashHoldings = Mathf.Clamp(newValue, 0f, Mathf.Infinity);
            OnCashHoldingsUpdated?.Invoke(currentCashHoldings);
        }

        void SavePlayerCash()
        {
            var path = Application.persistentDataPath + "/cashHoldings.rvve";

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Create);

            formatter.Serialize(stream, currentCashHoldings);
            stream.Close();
        }
    }
}