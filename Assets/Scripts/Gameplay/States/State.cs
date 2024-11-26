using System;
using UnityEngine;

namespace GamePlay.States
{
    public class State : MonoBehaviour
    {
        public StateType currentStateType;
        public StateType nextStateType;

        public Action OnStateComplete;

        [NaughtyAttributes.ReadOnly] public bool StateStarted = false;

        private bool logsEnabled = false;

        public virtual void StartState()
        {
            if (StateStarted) return;

            if (logsEnabled)
                Debug.Log($"<color=yellow>State Manager: State {currentStateType} started</color>");

            StateStarted = true;
        }

        protected virtual void EndState()
        {
            if (!StateStarted) return;
            
            if (logsEnabled)
                Debug.Log($"<color=yellow>State Manager:  State {currentStateType} ended</color>");

            StateStarted = false;

            OnStateComplete?.Invoke();
        }

        protected virtual void Update()
        {
            if (!StateStarted) return;
        }
    }
}