using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RvveSplit
{
    public class FinalPlayerCashHoldingsLoader : MonoBehaviour
    {
        public event Action<float> FinalCashHoldingsLoaded;

        private void Start()
        {
            LoadFinalPlayerCashHoldings();
        }

        void LoadFinalPlayerCashHoldings()
        {
            var path = Application.persistentDataPath + "/cashHoldings.rvve";
            if (!File.Exists(path)) return;

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            float finalCashHoldings = (float)formatter.Deserialize(stream);
            stream.Close();

            FinalCashHoldingsLoaded?.Invoke(finalCashHoldings);
        }
    }
}
