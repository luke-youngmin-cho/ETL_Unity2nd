using UnityEngine;

namespace FSM.AnimatorController
{
    public class StateBase : StateMachineBehaviour
    {
        protected CharacterController controller;

        public virtual void Initialize(CharacterController controller)
        {
            this.controller = controller;
        }


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetBool("isDirty", false);
        }

        public void ChangeState(Animator animator, State newState)
        {
            animator.SetInteger("state", (int)newState);
            animator.SetBool("isDirty", true);
        }
    }
}