using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RvveSplit.Cash
{
    public class PreviousScoreSaverAndLoader : MonoBehaviour
    {
        FinalPlayerCashHoldingsLoader finalPlayerCash;

        public event Action<float> PreviousScoreLoaded;

        private void OnEnable()
        {
            finalPlayerCash = FindObjectOfType<FinalPlayerCashHoldingsLoader>();
            finalPlayerCash.FinalCashHoldingsLoaded += SavePreviousScore;
        }

        private void OnDisable()
        {
            finalPlayerCash.FinalCashHoldingsLoaded -= SavePreviousScore;
        }

        private void Start()
        {
            LoadPreviousScore();
        }

        private void SavePreviousScore(float finalCashHoldings)
        {
            var path = Application.persistentDataPath + "/previousScore.rvve";

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Create);

            formatter.Serialize(stream, finalCashHoldings);
            stream.Close();
        }

        private float LoadPreviousScore()
        {
            var path = Application.persistentDataPath + "/previousScore.rvve";
            if (!File.Exists(path)) return 0f;

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            var previousScore = (float)formatter.Deserialize(stream);
            
            stream.Close();

            PreviousScoreLoaded?.Invoke(previousScore);
            
            return previousScore;
        }
    }
}
