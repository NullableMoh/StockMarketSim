using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RvveSplit
{
    public class GameWonNextSceneObject : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.LoadScene(3);
        }
    }
}
