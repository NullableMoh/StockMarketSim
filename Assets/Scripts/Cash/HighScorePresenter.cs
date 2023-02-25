using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RvveSplit.Cash
{
    public class HighScorePresenter : MonoBehaviour
    {
        HighscoreSaverAndLoader highScorePresenterAndLoader;
        TextMeshProUGUI text;

        private void OnEnable()
        {
            text = GetComponent<TextMeshProUGUI>();
            
            highScorePresenterAndLoader = FindObjectOfType<HighscoreSaverAndLoader>();
            highScorePresenterAndLoader.HighScoreCalculated += DisplayHighScore;
        }

        private void OnDisable()
        {
            highScorePresenterAndLoader.HighScoreCalculated -= DisplayHighScore;
        }

        private void DisplayHighScore(float highScore)
        {
            text.text = $"HIGH SCORE: ${highScore}";
        }
    }
}
