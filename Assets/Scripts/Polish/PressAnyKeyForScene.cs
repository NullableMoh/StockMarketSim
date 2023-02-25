using RvveSplit.Cash;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace RvveSplit.Polish
{
    public class PressAnyKeyForScene : MonoBehaviour
    {
        [SerializeField] int sceneIndex;

        private void Update()
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
