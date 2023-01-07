using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace RvveSplit
{
    public class StockOwnerHitNotification : MonoBehaviour
    {
        [SerializeField] float displayTime = 3f;

        CompetitorHandler competitorHandler;
        TextMeshProUGUI text;

        private void OnEnable()
        {
            competitorHandler = FindObjectOfType<CompetitorHandler>();
            competitorHandler.OnCompetitorShouldDie += DisplayHitSuccessfulNotification;
            competitorHandler.OnCompetitorShouldNotDie += DisplayHitFailedNotification;

            text = GetComponent<TextMeshProUGUI>();
            text.text = "";
        }

        private void OnDisable()
        {
            competitorHandler.OnCompetitorShouldDie -= DisplayHitSuccessfulNotification;
            competitorHandler.OnCompetitorShouldNotDie -= DisplayHitFailedNotification;
        }

        private void DisplayHitSuccessfulNotification(object sender, CompetitorShouldDieEventArgs e)
        {
            StartCoroutine(HitSuccessfulDisplayForTime(e.StockToDie.OwnerName, e.StockToDie.StockName));
        }


        private void DisplayHitFailedNotification(object sender, CompetitorShouldNotDieEventArgs e)
        {
            StartCoroutine(HitFailedDisplayForTime(e.StockToDie.OwnerName, e.StockToDie.StockName));
        }


        IEnumerator HitSuccessfulDisplayForTime(string competitorName, string stockName)
        {
            text.color = Color.black;
            text.text = $"{competitorName.ToUpper()} [OWNER OF] {stockName.ToUpper()} HAS BEEN KILLED";
            yield return new WaitForSeconds(displayTime);
            text.text = "";
        }

        IEnumerator HitFailedDisplayForTime(string competitorName, string stockName)
        {
            text.color = Color.white;
            text.text = $"HIT ON {competitorName.ToUpper()} FAILED";
            yield return new WaitForSeconds(displayTime);
            text.text = "";
        }
    }
}