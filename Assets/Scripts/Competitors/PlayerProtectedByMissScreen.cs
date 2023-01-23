using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class PlayerProtectedByMissScreen : MonoBehaviour
    {
        [SerializeField] GameObject objToEnable;

        PlayerArmor armor;

        private void OnEnable()
        {
            armor = FindObjectOfType<PlayerArmor>();
            armor.PlayerProtectedByMiss += EnableGameOverScreen;
        }

        private void OnDisable()
        {
            armor.PlayerProtectedByMiss -= EnableGameOverScreen;
        }

        private void EnableGameOverScreen()
        {
            objToEnable.SetActive(true);
        }
    }
}
