using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace RvveSplit
{
    public class MarketTimePresenter : MonoBehaviour
    {
        TextMeshProUGUI text;
        MarketClock clock;

        private void OnEnable()
        {
            text = GetComponent<TextMeshProUGUI>();
            clock = GetComponent<MarketClock>();
            clock.OnTimeUpdated += UpdateTimeUI;
        }

        private void OnDisable()
        {
            clock.OnTimeUpdated -= UpdateTimeUI;
        }

        private void UpdateTimeUI(int time)
        {
            text.text = $"{time / 60:00}:{time % 60:00}";
        }
    }
}