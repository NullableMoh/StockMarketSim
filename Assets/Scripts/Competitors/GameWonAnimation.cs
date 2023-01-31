using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class GameWonAnimation : MonoBehaviour
    {
        [SerializeField] AudioClip gameWonSound;
        [SerializeField] float timeTillDisable;

        const string GameWonAnim = "GameWonAnimation";
        const string NoAnim = "NoAnimation";

        string currentAnimState;

        Animator anim;
        AudioSource audioSource;
        TextMeshProUGUI text;

        private void OnEnable()
        {
            anim = GetComponent<Animator>();
            audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.volume = 0.3f;
            audioSource.playOnAwake = false;

            StartCoroutine(PlayAnimationWithSound());
        }

        IEnumerator PlayAnimationWithSound()
        {
            PlayAnimation(GameWonAnim);
            audioSource.PlayOneShot(gameWonSound);

            yield return new WaitForSeconds(timeTillDisable);

            PlayAnimation(NoAnim);
            gameObject.SetActive(false);
        }

        void PlayAnimation(string newState)
        {
            if (currentAnimState == newState) return;

            currentAnimState = newState;
            anim.Play(currentAnimState);
        }



    }
}
