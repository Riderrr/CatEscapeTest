using System;
using UnityEngine;

namespace Gameplay
{
    public class CatTriggerZone : MonoBehaviour
    {
        public LayerMask catLayerMask;

        public Action OnTriggerEnterAction;

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & catLayerMask) != 0)
            {
                OnTriggerEnterAction?.Invoke();
            }
        }
    }
}