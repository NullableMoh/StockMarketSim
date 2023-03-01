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
        [SerializeField] float waitTillLoad = 5f;

        public event Action<float> FinalCashHoldingsLoaded;

        private void Start()
        {
            StartCoroutine(LoadFinalPlayerCashHoldings());
        }

        IEnumerator LoadFinalPlayerCashHoldings()
        {
            var path = Application.persistentDataPath + "/cashHoldings.rvve";
            if (!File.Exists(path)) yield break;

            yield return new WaitForSeconds(waitTillLoad);

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            float finalCashHoldings = (float)formatter.Deserialize(stream);
            stream.Close();

            FinalCashHoldingsLoaded?.Invoke(finalCashHoldings);
        }
    }
}
