using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.States
{
    public enum StateType
    {
        InitState,
        PlaySessionState,
        CTAState,
    }
    
    public class StateManager : MonoBehaviour
    {
        
        public StateType startState;
        public StateType currentStateType;

        public List<State> states = new List<State>();

        public Action<StateType> OnStateChangeAction;

        //Make states queue
        private Queue<State> _statesQueue = new Queue<State>();

        private State _currentState;

        private void Awake()
        {
          
        }

        private void Start()
        {
            StartFirstState();
        }

        public void StartFirstState()
        {
            StartState(startState);
        }


        private void StartState(StateType stateType)
        {
            
            
            _currentState = GetStateByType(stateType);

            if (_currentState == null)
            {
                Debug.LogWarning("State is null");
                return;
            }

            currentStateType = stateType;
            
            _currentState.OnStateComplete += OnStateComplete;

            
            OnStateChangeAction?.Invoke(currentStateType);
            
            _currentState.StartState();
        }

        private State GetStateByType(StateType stateType)
        {
            return states.Find(state => state.currentStateType == stateType);
        }

        private void OnStateComplete()
        {
            _currentState.OnStateComplete -= OnStateComplete;
            StartState(_currentState.nextStateType);
        }
    }
}
