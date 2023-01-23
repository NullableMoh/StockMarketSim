using System.Collections;
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
            PlayAnimation(ArmorProtectAnim);
            audioSource.PlayOneShot(hitMissSound);

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

