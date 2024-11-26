using System;
using Dreamteck.Splines;
using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public string catchAnimationTrigger = "Catch";
        public string speedAnimationParameter = "Speed";

        public Action OnCatCaughtAction;
        
        private SplineFollower _splineFollower;
        private Animator _animator;
        private CatchZoneTrigger _catchZoneTrigger;

        private void Awake()
        {
            _splineFollower = GetComponent<SplineFollower>();
            _animator = GetComponentInChildren<Animator>();
            _catchZoneTrigger = GetComponentInChildren<CatchZoneTrigger>();
        }

        public void SetMoveSpeed(float speed)
        {
            _splineFollower.followSpeed = speed;
        }

        private void OnEnable()
        {
            _catchZoneTrigger.OnTriggerEnterAction += StopMovement;
        }

        private void OnDisable()
        {
            _catchZoneTrigger.OnTriggerEnterAction -= StopMovement;
        }

        private void StopMovement()
        {
            _splineFollower.follow = false;
            _animator.SetTrigger(catchAnimationTrigger);
            _animator.SetFloat(speedAnimationParameter, 0);
            OnCatCaughtAction?.Invoke();
        }

        private void UpdateAnimation()
        {
            if (_splineFollower.follow)
                _animator.SetFloat(speedAnimationParameter, _splineFollower.followSpeed);
        }

        private void Update()
        {
            UpdateAnimation();
        }
    }
}