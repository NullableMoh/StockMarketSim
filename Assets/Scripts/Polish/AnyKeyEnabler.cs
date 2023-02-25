using RvveSplit.Cash;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Polish
{
    public class AnyKeyEnabler : MonoBehaviour
    {
        [SerializeField] GameObject objToEnable;

        FinalPlayerCashHoldingsPresenter finalCashPresenter;
        private void OnEnable()
        {
            finalCashPresenter = FindObjectOfType<FinalPlayerCashHoldingsPresenter>();
            finalCashPresenter.PlayerCashFinishedLerp += EnableAnyKeyPressedObj;
        }

        private void OnDisable()
        {
            finalCashPresenter.PlayerCashFinishedLerp -= EnableAnyKeyPressedObj;
        }

        private void EnableAnyKeyPressedObj()
        {
            objToEnable.SetActive(true);
        }
    }
}
