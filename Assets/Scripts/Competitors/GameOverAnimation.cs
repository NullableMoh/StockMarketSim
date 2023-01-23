using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class GameOverAnimation : MonoBehaviour
    {
        [SerializeField] AudioClip gameOverSound;
        [SerializeField] float timeTillDisable;

        const string GameOverAnim = "GameOver";
        const string NoAnim = "NoAnimation";
        public string KillerName { private get; set; }

        string currentAnimState;

        Animator anim;
        AudioSource audioSource;
        TextMeshProUGUI text;

        private void OnEnable()
        {
            anim= GetComponent<Animator>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.volume = 0.3f;
            audioSource.playOnAwake = false;

            StartCoroutine(PlayAnimationWithSound());
        }

        IEnumerator PlayAnimationWithSound()
        {
            PlayAnimation(GameOverAnim);
            audioSource.PlayOneShot(gameOverSound);
            text.text = $"KILLED BY {KillerName}";

            yield return new WaitForSeconds(timeTillDisable);

            text.text = "";
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
