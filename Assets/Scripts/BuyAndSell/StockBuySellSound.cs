using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.BuyAndSell
{
    public class StockBuySellSound : MonoBehaviour
    {
        [SerializeField] AudioClip transactionSuccessful;
        [SerializeField] AudioClip transactionFailed;

        AudioSource audioSource;
        StockPricePlayerCashMiddleMan man;

        private void OnEnable()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.3f;
            audioSource.playOnAwake = false;

            man = GetComponent<StockPricePlayerCashMiddleMan>();
            man.CanBuy += PlayTransactionSuccessfulSound;
            man.CanSell += PlayTransactionSuccessfulSound;
            man.CannotBuy += PlayTransactionFailedSound;
            man.CannotSell += PlayTransactionFailedSound;
        }

        private void OnDisable()
        {
            man.CanBuy -= PlayTransactionSuccessfulSound;
            man.CanSell -= PlayTransactionSuccessfulSound;
            man.CannotBuy -= PlayTransactionFailedSound;
            man.CannotSell -= PlayTransactionFailedSound;
        }

        void PlayTransactionSuccessfulSound(float _)
        {
            audioSource.PlayOneShot(transactionSuccessful);
        }

        void PlayTransactionFailedSound()
        {
            audioSource.PlayOneShot(transactionFailed);
        }
    }
}
