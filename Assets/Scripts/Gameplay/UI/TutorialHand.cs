using UnityEngine;

namespace Gameplay.UI
{
    public class TutorialHand : MonoBehaviour
    {
        public Transform target;
        public float radius = 100f;
        public float speed = 1f;

        private RectTransform _rect;
        private Vector2 _startPosition;

        void Awake()
        {
            _rect = target.GetComponent<RectTransform>();
            _startPosition = _rect.anchoredPosition;
        }

        void Update()
        {
            float t = Time.time * speed;
            float s = 2f / (3f - Mathf.Cos(t * 2f));
            float x = s * Mathf.Cos(t) * 2f * radius;
            float y = s * Mathf.Sin(t * 2f) * radius;

            _rect.anchoredPosition = _startPosition + new Vector2(x, y);
        }
    }
}