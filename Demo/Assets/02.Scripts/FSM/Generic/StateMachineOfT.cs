using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Generic
{
    public class StateMachine<T>
    where T : Enum
    {
        public GameObject owner;
        public T current;
        public Dictionary<T, IState<T>> states;
        private bool _isDirty;

        public StateMachine(GameObject owner)
        {
            this.owner = owner;
        }


        public bool ChangeState(T newID)
        {
            // Don't change if state has already changed in this frame.
            if (_isDirty)
                return false;

            // Don't change if target state is same with current.
            if (Comparer<T>.Default.Compare(newID, current) == 0)
                return false;

            // Don't change if target state is not available
            if (states[newID].canExecute == false)
                return false;

            states[current].OnExit(); // Exit from previous state
            current = newID; // Refresh state
            states[current].OnEnter(); // Enter to new state
            _isDirty = true;
            return true;
        }

        protected virtual void Initialize(IEnumerable<KeyValuePair<T, IState<T>>> copy)
        {
            this.states = new Dictionary<T, IState<T>>(copy);
        }

        public void Update()
        {
            ChangeState(states[current].OnUpdate());
        }

        public void FixedUpdate()
        {
            states[current].OnFixedUpdate();
        }

        public void LateUpdate()
        {
            _isDirty = false;
        }
    }
}