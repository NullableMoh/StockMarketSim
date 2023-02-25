using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RvveSplit.SceneManagement
{
    public class CheckHighScoreButton : MonoBehaviour
    {
        [SerializeField] int highScoreSceneIndex = 3;

        public void CheckHighScore()
        {
            SceneManager.LoadScene(highScoreSceneIndex);
        }
    }
}
