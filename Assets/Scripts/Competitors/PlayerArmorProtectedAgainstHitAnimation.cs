using System.Collections;
using TMPro;
using UnityEngine;

namespace RvveSplit.Competitors
{
    public class PlayerArmorProtectedAgainstHitAnimation : MonoBehaviour
    {
        [SerializeField] AudioClip armorDeflectSound;
        [SerializeField] float timeTillDisable;

        const string ArmorProtectAnim = "ArmorProtect";
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
            audioSource.PlayOneShot(armorDeflectSound);
            text.text = "ARMOR PROTECTED YOU " + (!string.IsNullOrEmpty(HitCallerName) ? "AGAINST " : "") + HitCallerName;

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

