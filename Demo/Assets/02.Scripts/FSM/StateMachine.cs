using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSM
{
    public class StateMachine
    {
        public GameObject owner;
        public int current;
        public Dictionary<int, IState> states;
        private bool _isDirty;

        public StateMachine(GameObject owner)
        {
            this.owner = owner; // 이 기계의 주인은 나다
        }

        public bool ChangeState(int newID)
        {
            // 이미 현재 프레임에서 상태가 바뀐적이 있다면 바꾸지않겠다
            if (_isDirty)
                return false;

            // 바꾸려는 상태가 현재 상태라면 바꾸지 않겠다
            if (newID == current)
                return false;

            // 바꾸려는 상태가 실행가능하지 않다면 바꾸지않겠다.
            if (states[newID].canExecute == false)
                return false;

            states[current].OnExit(); // 기존 상태에서 나옴
            current = newID; // 상태 갱신
            states[current].OnEnter(); // 새로운 상태로 진입
            _isDirty = true;
            Debug.Log($"Changed state to {newID}");
            return true;
        }

        protected virtual void Initialize(IEnumerable<KeyValuePair<int, IState>> copy)
        {
            this.states = new Dictionary<int, IState>(copy);
            current = states.First().Key;
            states[current].OnEnter();
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
