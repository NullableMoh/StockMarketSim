using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class GameWonActivator : MonoBehaviour
    {
        [SerializeField] GameObject gameWonAnimObjToEnable;

        CompetitorHandler competitorHandler;

        public event Action GameWon;

        private void OnEnable()
        {
            competitorHandler = FindObjectOfType<CompetitorHandler>();
            competitorHandler.OnAllCompetitorsKilled += EnableGameWonAnim;
        }

        private void OnDisable()
        {
            competitorHandler.OnAllCompetitorsKilled -= EnableGameWonAnim;
        }

        private void EnableGameWonAnim(object sender, AllCompetitorsKilledEventArgs e)
        {
            StartCoroutine(EnableGameWonAnimation());
        }

        IEnumerator EnableGameWonAnimation()
        {
            GameWon?.Invoke();
            yield return new WaitForSeconds(0f);
            gameWonAnimObjToEnable.SetActive(true);
        }
    }
}
