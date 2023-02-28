using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RvveSplit.Polish
{
    public class AnyKeyIntroSceneEnabler : MonoBehaviour
    {
        [SerializeField] GameObject objToEnable;

        LetterRevealer revealer;
        private void OnEnable()
        {
            revealer = FindObjectOfType<LetterRevealer>();
            revealer.LetterRevealed += EnableObject;
        }

        private void OnDisable()
        {
            revealer.LetterRevealed -= EnableObject;
        }

        void EnableObject()
        {
            objToEnable.SetActive(true);
        }
    }
}
