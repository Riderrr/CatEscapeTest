using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        public string speedAnimationParameter = "Speed";

        public float rotationSpeed = 0.001f;
        public float gravity = 9.8f;

        public float pawMaxGenerationTime = 0.5f;
        public float pawMinGenerationTime = 0.1f;

        public Transform viewTransform;
        public GameObject destroySfx;

        public Transform pawParent;
        public GameObject pawPrefab;

        [HideInInspector] public bool isMoving = false;

        private float _moveSpeed = 3f;
        private float _pawGenerationTimer;
        private Vector3 _deltaMove;
        private bool _isInputPressed;
        private bool _isDead;

        private Animator _animator;
        private CharacterController _characterController;
        private FloatingJoystick _floatingJoystick;


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _characterController = GetComponent<CharacterController>();
            _floatingJoystick = FindFirstObjectByType<FloatingJoystick>();
        }

        public void SetMoveSpeed(float speed)
        {
            _moveSpeed = speed;
        }

        public void Catch(Action cb)
        {
            viewTransform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);

            StartCoroutine(SetTimeOut(1f, () =>
            {
                if (cb != null) cb();
                Destroy(gameObject);
            }));

            destroySfx.SetActive(true);

            _isDead = true;
        }

        IEnumerator SetTimeOut(float time, Action cb)
        {
            yield return new WaitForSeconds(time);
            cb();
        }

        private void AddPawn()
        {
            GameObject paw = Instantiate(pawPrefab, transform.position, transform.rotation);

            paw.transform.Rotate(0, -90, 0);

            paw.transform.SetParent(pawParent);
            paw.transform.position = new Vector3(paw.transform.position.x, 0.01f, paw.transform.position.z);
        }

        private void UpdateMove()
        {
            isMoving = _floatingJoystick.isPressed;

            if (_floatingJoystick.isPressed)
            {
                _pawGenerationTimer -= Time.deltaTime;

                if (_pawGenerationTimer <= 0 && _deltaMove.sqrMagnitude > 0.1f)
                {
                    AddPawn();
                    _pawGenerationTimer = Random.Range(pawMinGenerationTime, pawMaxGenerationTime);
                }

                _deltaMove.x = _floatingJoystick.Horizontal;
                _deltaMove.z = _floatingJoystick.Vertical;
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(new Vector3(_deltaMove.x, 0, _deltaMove.z)),
                    rotationSpeed * Time.deltaTime);

                if (new Vector3(_deltaMove.x, 0, _deltaMove.z).sqrMagnitude < 0.1f)
                {
                    _deltaMove.x = 0;
                    _deltaMove.z = 0;
                }
            }
            else
            {
                _deltaMove.x = 0;
                _deltaMove.z = 0;
                _pawGenerationTimer = Random.Range(pawMinGenerationTime, pawMaxGenerationTime);
            }

            Vector3 moveAxis = new Vector3(_deltaMove.x, -gravity, _deltaMove.z);
            _characterController.Move(((moveAxis) * (_moveSpeed * Time.deltaTime)));

            _animator.SetFloat(speedAnimationParameter, _characterController.velocity.magnitude);
        }

        void FixedUpdate()
        {
            if (!_isDead)
                UpdateMove();
        }
    }
}