using UnityEngine;

namespace FSM.AnimatorController
{
    public class Move : StateBase
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (controller.isGrounded == false)
            {
                ChangeState(animator, State.Fall);
            }
        }
    }
}
