using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASSNStock : MonoBehaviour
{
    CompetitorHandler competitorHandler;

    private void OnEnable()
    {
        competitorHandler = FindObjectOfType<CompetitorHandler>();
        competitorHandler.OnAllCompetitorsKilled += DestroySelf;
    }

    private void OnDisable()
    {
        competitorHandler.OnAllCompetitorsKilled -= DestroySelf;
    }


    private void DestroySelf(object sender, AllCompetitorsKilledEventArgs e)
    {
        Destroy(gameObject);
    }
}
