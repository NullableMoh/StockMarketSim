using RvveSplit.Competitors;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class PlayerArmorProtectedAgainstHitScreen : MonoBehaviour
    {
        [SerializeField] GameObject objToEnable;

        PlayerArmor armor;

        private void OnEnable()
        {
            armor = FindObjectOfType<PlayerArmor>();
            armor.PlayerArmorProtectedAgainstHit += EnableGameOverScreen;
        }

        private void OnDisable()
        {
            armor.PlayerArmorProtectedAgainstHit -= EnableGameOverScreen;
        }

        private void EnableGameOverScreen(string hitCallerName)
        {
            objToEnable.SetActive(true);
            var playerProtectedByArmorHitAnim = objToEnable.GetComponent<PlayerArmorProtectedAgainstHitAnimation>();
            if (playerProtectedByArmorHitAnim)
            {
                playerProtectedByArmorHitAnim.HitCallerName = hitCallerName;
            }
        }
    }
}

