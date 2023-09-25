using UnityEngine;

namespace FSM
{
    public abstract class StateBase : IState
    {
        public const int MOVE = 1;
        public const int JUMP = 2;
        public const int FALL = 3;



        public int id { get; private set; }

        public virtual bool canExecute => true;

        protected PlayerController controller;
        protected StateMachine machine;
        protected Transform transform;
        protected Rigidbody rigidbody;
        protected Animator animator;

        private bool _hasFixedUpdatedAtVeryFirst;

        public StateBase(int id, StateMachine machine)
        {
            this.id = id;
            this.machine = machine;
            this.controller = machine.owner.GetComponent<PlayerController>();   
            this.transform = machine.owner.GetComponent<Transform>();
            this.rigidbody = machine.owner.GetComponent<Rigidbody>();
            this.animator = machine.owner.GetComponentInChildren<Animator>();
        }

        public virtual void OnEnter()
        {
            _hasFixedUpdatedAtVeryFirst = false;
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnFixedUpdate()
        {
            if (_hasFixedUpdatedAtVeryFirst == false)
                _hasFixedUpdatedAtVeryFirst = true;
        }

        public virtual int OnUpdate()
        {
            return _hasFixedUpdatedAtVeryFirst ? id : -1;
        }
    }
}
