using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace RvveSplit.BlackMarketAnimations
{
    public class BlackMarketKillAnim : MonoBehaviour
    {
        [SerializeField] float timeTillDisable = 3f;
        [SerializeField] AudioClip sniperShot;

        const string BlackMarketAnimation = "BlackMarketAnimation";
        const string NoAnimation = "NoAnimation";
        string currentState;

        Animator anim;
        AudioSource audioSource;

        private void OnEnable()
        {
            anim = GetComponent<Animator>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.3f;
            audioSource.playOnAwake = false;

            StartCoroutine(OnEnableCoroutine());
        }

        IEnumerator OnEnableCoroutine()
        {
            PlayAnimation(BlackMarketAnimation);
            audioSource.PlayOneShot(sniperShot);

            yield return new WaitForSeconds(timeTillDisable);

            PlayAnimation(NoAnimation);
            gameObject.SetActive(false);
        }

        void PlayAnimation(string newState)
        {
            if (newState != currentState)
            {
                currentState = newState;
                anim.Play(currentState);
            }
        }
    }
}