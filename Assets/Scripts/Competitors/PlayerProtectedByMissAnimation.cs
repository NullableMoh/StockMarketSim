using System.Collections;
using TMPro;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class PlayerProtectedByMissAnimation : MonoBehaviour
    {
        [SerializeField] AudioClip hitMissSound;
        [SerializeField] float timeTillDisable;

        const string ArmorProtectAnim = "MissProtect";
        const string NoAnim = "NoAnimation";
        public string HitCallerName { private get; set; }

        string currentAnimState;

        Animator anim;
        AudioSource audioSource;
        TextMeshProUGUI text;

        private void OnEnable()
        {
            anim = GetComponent<Animator>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.volume = 0.3f;
            audioSource.playOnAwake = false;


            StartCoroutine(PlayAnimationWithSound());
        }

        IEnumerator PlayAnimationWithSound()
        {
            PlayAnimation(ArmorProtectAnim);
            text.text = "PLAYER PROTECTED BY MISS";
            audioSource.PlayOneShot(hitMissSound);

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

