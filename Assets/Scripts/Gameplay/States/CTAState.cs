using Gameplay;
using Gameplay.UI;
using UnityEngine;

namespace GamePlay.States
{
    public class CTAState : State
    {
        //TODO remake. Add pass parameter to state
        [HideInInspector] public bool isWin = false;

        private CTA _cta;

        private void Awake()
        {
            if (_cta == null)
                _cta = FindFirstObjectByType<CTA>();
        }

        public override void StartState()
        {
            base.StartState();

            _cta.ShowCTA(isWin);
        }
    }
}