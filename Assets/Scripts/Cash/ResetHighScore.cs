using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RvveSplit.Cash
{
    public class ResetHighScore : MonoBehaviour
    {
        [SerializeField] int mainMenuSceneIndex = 0;

        public void ResetScore()
        {
            var path = Application.persistentDataPath + "/highScore.rvve";

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Create);

            float score = -1f;

            formatter.Serialize(stream, score);
            stream.Close();

            var pathPrev = Application.persistentDataPath + "/previousScore.rvve";

            BinaryFormatter formatterPrev = new();
            FileStream streamPrev = new(pathPrev, FileMode.Create);

            float scorePrev = 0f;

            formatter.Serialize(streamPrev, scorePrev);
            streamPrev.Close();

            SceneManager.LoadScene(mainMenuSceneIndex);
        }
    }
}
