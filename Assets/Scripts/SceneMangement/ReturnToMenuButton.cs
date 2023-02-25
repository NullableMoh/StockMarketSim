using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RvveSplit.SceneManagement
{
    public class ReturnToMenuButton : MonoBehaviour
    {
        [SerializeField] int mainMenuSceneIndex = 0;

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
    }
}
