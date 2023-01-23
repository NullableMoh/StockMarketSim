using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class PlayerArmor : MonoBehaviour
    {
        int armor;

        RandomHitsOnPlayer randomHitsOnPlayer;

        public event Action<string> PlayerArmorProtectedAgainstHit;
        public event Action<string> PlayerKilledByRandomHit;
        public event Action PlayerProtectedByMiss;

        private void OnEnable()
        {
            randomHitsOnPlayer = FindObjectOfType<RandomHitsOnPlayer>();
            randomHitsOnPlayer.OnRandomHit += HandleRandomHit;
        }

        private void OnDisable()
        {
            randomHitsOnPlayer.OnRandomHit -= HandleRandomHit;
        }

        void HandleRandomHit(object sender, RandomHitAttemptedEventArgs e)
        {
            if(!e.HitSuccessful)
                PlayerProtectedByMiss?.Invoke();
            
            else if(armor > 0)
            {
                PlayerArmorProtectedAgainstHit?.Invoke(e.CompetitorStockName);
                armor--;
            }

            else
                PlayerKilledByRandomHit?.Invoke(e.CompetitorStockName);
        }
    }
}
