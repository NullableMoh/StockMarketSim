using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RvveSplit.Cash
{
    public class FinalPlayerCashHoldingsPresenter : MonoBehaviour
    {
        [SerializeField] float lerpDuration = 5f;
        [SerializeField] AudioClip moneyCollected;

        TextMeshProUGUI text;
        float finalHoldings;
        float currentHoldings;
        float timeOfPlay;

        FinalPlayerCashHoldingsLoader loader;
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.volume = 0.3f;
            audioSource.playOnAwake = false;

            timeOfPlay = 0;
        }

        private void OnEnable()
        {
            text = GetComponent<TextMeshProUGUI>();
            
            loader = FindObjectOfType<FinalPlayerCashHoldingsLoader>();
            loader.FinalCashHoldingsLoaded += UpdateFinalPlayerCashHoldings;
        }

        private void OnDisable()
        {
            loader.FinalCashHoldingsLoaded -= UpdateFinalPlayerCashHoldings;
        }

        void UpdateFinalPlayerCashHoldings(float finalPlayerCashHoldings)
        {
            finalHoldings = finalPlayerCashHoldings;
            StartCoroutine(LerpPlayerCashHoldings());
        }

        IEnumerator LerpPlayerCashHoldings()
        {
            var time = 0f;
            while(time < lerpDuration)
            {
                currentHoldings = Mathf.Lerp(0f, finalHoldings, time/lerpDuration);
                text.text = $"Final Player Cash Holdings: ${currentHoldings:0.00}";

                time += Time.deltaTime;
                PlaySound(moneyCollected);
                yield return null;
            }

            currentHoldings = finalHoldings;
            text.text = $"Final Player Cash Holdings: ${currentHoldings:0.00}";
            yield break;
        }

        void PlaySound(AudioClip clip)
        {
            if(Time.time <= timeOfPlay + clip.length) return;

            timeOfPlay = Time.time;
            audioSource.PlayOneShot(moneyCollected);
        }
    }
}
