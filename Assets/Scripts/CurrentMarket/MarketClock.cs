using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RvveSplit.CurrentMarket
{
    public class MarketClock : MonoBehaviour
    {
        [SerializeField] int startingTimeInMinutes;
        [SerializeField] float lengthOfMinute;

        int time;

        public event Action<int> OnTimeUpdated;

        private void Awake()
        {
            //-1 because time is immediately increased by 1 in UpdateTime
            SetTime(startingTimeInMinutes - 1);

            StartCoroutine(UpdateTime());
        }

        IEnumerator UpdateTime()
        {
            while (true)
            {
                SetTime(time + 1);
                yield return new WaitForSeconds(lengthOfMinute);
            }
        }

        void SetTime(int newTime)
        {
            if (newTime >= 1440f)
            {
                newTime = 0;
            }

            time = newTime;

            OnTimeUpdated?.Invoke(time);
        }
    }
}