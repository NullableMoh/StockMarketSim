using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace RvveSplit.Polish
{
    public class LetterRevealer : MonoBehaviour
    {
        [TextArea(10,15)] [SerializeField] string letter;
        [SerializeField] float waitBetweenLetterReveal = 0.1f;
        [SerializeField] AudioClip letterRevealSound;
        [SerializeField] float volume = 0.2f;

        int letterIndex;
        float timeOfPlay;

        TextMeshProUGUI text;
        AudioSource audioSource;

        public event Action LetterRevealed;

        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;

            audioSource.loop = false;
            audioSource.playOnAwake= false;

            StartCoroutine(RevealText());
        }

        IEnumerator RevealText()
        {
            while(true)
            {
                if(letterIndex < letter.Length)
                {
                    text.text = letter.Substring(0, letterIndex);
                    PlayLetterRevealSound();
                    letterIndex++;
                    yield return new WaitForSeconds(waitBetweenLetterReveal);
                }
                else
                {
                    StopAudio();
                    LetterRevealed?.Invoke();
                    yield break;
                }
            }
        }

        void PlayLetterRevealSound()
        {
            if(Time.time > letterRevealSound.length + timeOfPlay)
            {
                audioSource.PlayOneShot(letterRevealSound);
                timeOfPlay = Time.time;
            }
        }

        void StopAudio()
        {
            audioSource.Stop();
            audioSource.enabled = false;
        }
    }
}
