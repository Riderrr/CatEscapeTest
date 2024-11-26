using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PawController : MonoBehaviour
    {
        MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            StartFade();
        }

        private void StartFade()
        {
            _meshRenderer.material
                .DOFade(0f, 2f).OnComplete(() => { Destroy(gameObject); });

            Debug.Log(_meshRenderer.material);
        }
    }
}