using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace RvveSplit.Polish
{
    public class TextRotater : MonoBehaviour
    {
        [SerializeField] float rotateSpeed = 0.5f;

        float direction;

        private void Awake()
        {
            direction = -1;
        }

        // Update is called once per frame
        void Update()
        {
            direction = (transform.localScale.x >= 1) ? -1 : (transform.localScale.x <= -1) ? 1 : direction;

            transform.localScale += Vector3.right * direction * rotateSpeed * Time.deltaTime;
        }
    }
}
