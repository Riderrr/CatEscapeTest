using UnityEngine;

namespace Gameplay.UI
{
    public class TutorialView : MonoBehaviour
    {
        public CanvasGroup tutorialCanvasGroup;
        public float updateSpeed = 3f;
        
        private float _targetAlpha = 0;

        public void ShowTutorial(bool force = false)
        {
            AnimateTutorialVisibility(true, force);
        }

        public void HideTutorial(bool force = false)
        {
            AnimateTutorialVisibility(false, force);
        }

        private void AnimateTutorialVisibility(bool state, bool force = false)
        {
            _targetAlpha = state ? 1 : 0;

            if (force)
                tutorialCanvasGroup.alpha = _targetAlpha;
        }

        private void Update()
        {
            tutorialCanvasGroup.alpha =
                Mathf.Lerp(tutorialCanvasGroup.alpha, _targetAlpha, updateSpeed * Time.deltaTime);
        }
    }
}