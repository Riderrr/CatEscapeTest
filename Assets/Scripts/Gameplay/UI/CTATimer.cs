using System;
using Settings;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class CTATimer : MonoBehaviour
    {
        public CreativeSettings creativeSettings;
        public TextMeshProUGUI timerText;
        public GameObject timerContainer;
        public Action OnTimerEndAction;

        private float _timer;
        private bool _canUpdate = false;

        private void Start()
        {
            _timer = creativeSettings.timeToCTA;
            
            UpdateTimerText();
            timerContainer.SetActive(creativeSettings.useTimer && creativeSettings.showTimerUI);
        }

        public void StartTimer()
        {
            if (creativeSettings.useTimer)
            {
                _canUpdate = true;
            }
        }

        private void UpdateTimer()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                OnTimerEndAction?.Invoke();
            }

            UpdateTimerText();
        }
        
        private void UpdateTimerText()
        {
            timerText.text = TimeSpan.FromSeconds(_timer).ToString(@"mm\:ss");
        }

        private void Update()
        {
            if (!_canUpdate) return;

            UpdateTimer();
        }
    }
}