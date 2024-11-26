using System.Collections.Generic;
using Gameplay;
using Gameplay.Character.Enemy;
using Gameplay.Character.Player;
using Gameplay.UI;
using Settings;
using UnityEngine;

namespace GamePlay.States
{
    public class PlaySessionState : State
    {
        public CreativeSettings creativeSettings;

        private List<EnemyController> _enemyControllers = new List<EnemyController>();
        private PlayerController _playerController;
        private ExitDoorTrigger _exitDoorTrigger;
        private TutorialView _tutorial;
        private CTATimer _ctaTimer;

        //TODO remake. Add pass parameter to state
        private CTAState _ctaState;
        private float _hintTime;

        private float _ctaTime;


        private void Awake()
        {
            _enemyControllers.Clear();
            _enemyControllers = new List<EnemyController>(FindObjectsByType<EnemyController>(FindObjectsSortMode.None));

            if (_playerController == null)
                _playerController = FindFirstObjectByType<PlayerController>();
            _playerController = FindFirstObjectByType<PlayerController>();

            if (_exitDoorTrigger == null)
                _exitDoorTrigger = FindFirstObjectByType<ExitDoorTrigger>();

            if (_tutorial == null)
                _tutorial = FindFirstObjectByType<TutorialView>();

            if (_ctaTimer == null)
                _ctaTimer = FindFirstObjectByType<CTATimer>();

            //TODO remake. Add pass parameter to state
            if (_ctaState == null)
                _ctaState = FindFirstObjectByType<CTAState>();
        }


        public override void StartState()
        {
            base.StartState();

            _enemyControllers.ForEach(enemyController => enemyController.OnCatCaughtAction += OnCatchCat);
            _ctaTimer.OnTimerEndAction += OnLoseLevel;
            _exitDoorTrigger.OnTriggerEnterAction += OnWinLevel;

            if (creativeSettings.showTutorial)
            {
                _tutorial.ShowTutorial(true);
            }
        }

        private void OnCatchCat()
        {
            _playerController.Catch(() => { OnLoseLevel(); });
        }
        
        private void UpdateHintAppear()
        {
            if (_playerController.isMoving)
            {
                _hintTime = 0;
                _tutorial.HideTutorial(true);
                return;
            }
            
            _hintTime += Time.deltaTime;
            if (_hintTime >= creativeSettings.hintTime)
            {
                _tutorial.ShowTutorial();
            }
        }

        private void CheckForInput()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _ctaTimer.StartTimer();
            }
        }
        
        private void OnWinLevel()
        {
            _ctaState.isWin = true;
            EndState();
        }

        private void OnLoseLevel()
        {
            _ctaState.isWin = false;
            EndState();
        }

        protected override void Update()
        {
            if (creativeSettings.showHint)
                UpdateHintAppear();
            
            CheckForInput();
        }
        
        protected override void EndState()
        {
            if (!StateStarted) return;
            _enemyControllers.ForEach(enemyController => enemyController.OnCatCaughtAction -= OnCatchCat);
            _exitDoorTrigger.OnTriggerEnterAction -= OnWinLevel;
            _ctaTimer.OnTimerEndAction -= OnLoseLevel;

            base.EndState();
        }
    }
}