using Unity.VisualScripting;
using UnityEngine;

namespace FSM
{
    public class Jump : StateBase
    {
        public override bool canExecute => base.canExecute &&
                                           controller.isGrounded &&
                                           machine.current == 1;

        private float _force;

        public Jump(int id, StateMachine machine, float force) : base(id, machine)
        {
            _force = force;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            controller.isMovable = false;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z);
            rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
            animator.Play("Jump");
        }

        public override int OnUpdate()
        {
            int next = base.OnUpdate();

            if (next < 0)
                return id;

            if (rigidbody.velocity.y <= 0)
                next = FALL;

            return next;
        }
    }
}
