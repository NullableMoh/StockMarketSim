using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace RvveSplit.Cash
{
    public class HighscoreSaverAndLoader : MonoBehaviour
    {
        public event Action<float> HighScoreCalculated;

        // Start is called before the first frame update
        void Start()
        {
            CalculateNewHighScore();
        }


        void CalculateNewHighScore()
        {
            float previousScore;
            var previousScorePath = Application.persistentDataPath + "/previousScore.rvve";
            if (!File.Exists(previousScorePath))
            {
                previousScore = 0f;
            }
            else
            {
                BinaryFormatter previousScoreFormatter = new();
                FileStream previousScoreStream = new(previousScorePath, FileMode.Open);

                previousScore = (float)previousScoreFormatter.Deserialize(previousScoreStream);
                previousScoreStream.Close();
            }

            var highScore = LoadHighScore();

            if(previousScore > highScore)
            {
                SaveHighScore(previousScore);
                HighScoreCalculated?.Invoke(previousScore);
            }
            else
            {
                SaveHighScore(highScore);
                HighScoreCalculated?.Invoke(highScore);
            }
        }


        float LoadHighScore()
        {
            var highScorePath = Application.persistentDataPath + "/highScore.rvve";
            if (!File.Exists(highScorePath))
            {
                return -1f;

            }


            BinaryFormatter highScoreFormatter = new();
            FileStream highScoreStream = new(highScorePath, FileMode.Open);

            float highScore;

            var highScoreDeserialized = highScoreFormatter.Deserialize(highScoreStream).ToString();

            if (!Single.TryParse(highScoreDeserialized, out var _))
            {
                highScore = 0f;
            }
            else
            {
                highScore = float.Parse(highScoreDeserialized);
            }

            highScoreStream.Close();

            return highScore;
        }

        void SaveHighScore(float score)
        {
            var highScorePath = Application.persistentDataPath + "/highScore.rvve";

            BinaryFormatter highScoreFormatter = new();
            FileStream highScoreStream = new(highScorePath, FileMode.Create);

            highScoreFormatter.Serialize(highScoreStream, score);
            
            highScoreStream.Close();
        }
    }

}
