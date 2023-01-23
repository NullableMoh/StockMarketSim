using RvveSplit.Competitors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] GameObject objToEnable;

        PlayerArmor armor;
        
        private void OnEnable()
        {
            armor = FindObjectOfType<PlayerArmor>();
            armor.PlayerKilledByRandomHit += EnableGameOverScreen;
        }

        private void OnDisable()
        {
            armor.PlayerKilledByRandomHit -= EnableGameOverScreen;
        }

        private void EnableGameOverScreen(string killerName)
        {
            objToEnable.SetActive(true);
            var gameOverAnim = objToEnable.GetComponent<GameOverAnimation>();
            if(gameOverAnim)
            {
                gameOverAnim.KillerName = killerName;
            }
        }
    }
}
