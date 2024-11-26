using System.Collections.Generic;
using Gameplay.Character.Enemy;
using Gameplay.Character.Player;
using Settings;
using UnityEngine;

namespace GamePlay.States
{
    public class InitState : State
    {
        public CreativeSettings creativeSettings;
        private PlayerController _playerController;
        private List<EnemyController> _enemyControllers = new List<EnemyController>();
        private FloatingJoystick _floatingJoystick;

        private void Awake()
        {
            if (_floatingJoystick == null)
                _floatingJoystick = FindFirstObjectByType<FloatingJoystick>();

            if (_playerController == null)
                _playerController = FindFirstObjectByType<PlayerController>();

            _enemyControllers.Clear();
            _enemyControllers = new List<EnemyController>(FindObjectsByType<EnemyController>(FindObjectsSortMode.None));
        }

        public override void StartState()
        {
            base.StartState();

            _playerController.SetMoveSpeed(creativeSettings.playerMoveSpeed);

            _enemyControllers.ForEach(enemyController => enemyController.SetMoveSpeed(creativeSettings.enemyMoveSpeed));
            _floatingJoystick.SetBackgroundTransparency(creativeSettings.showJoystick ? 0.75f : 0f);

            EndState();
        }


        protected override void EndState()
        {
            if (!StateStarted) return;

            base.EndState();
        }
    }
}