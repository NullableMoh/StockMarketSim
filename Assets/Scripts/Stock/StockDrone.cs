using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RvveSplit.Stock
{
    public class StockDrone : MonoBehaviour
    {
        [SerializeField] AudioClip droneSound;

        AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = droneSound;
            audioSource.playOnAwake = true;
            audioSource.loop = true;
        }

        private void Start()
        {
            audioSource.Play();
        }

        void Update()
        {
            UpdateVolume();
        }

        void UpdateVolume()
        {
            var mouseToStockDistance = Mathf.Abs(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y - transform.position.y - 2);
            audioSource.volume = mouseToStockDistance != 0 ? 1f / mouseToStockDistance : 1f;
        }
    }
}
