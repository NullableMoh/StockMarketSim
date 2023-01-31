using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RvveSplit.SceneManagement
{
    public class QuitGameButton : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
