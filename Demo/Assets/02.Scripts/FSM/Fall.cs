namespace FSM
{
    public class Fall : StateBase
    {
        public Fall(int id, StateMachine machine) : base(id, machine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            controller.isMovable = false;
            animator.Play("Fall");
        }

        public override int OnUpdate()
        {
            int next = base.OnUpdate();

            if (next < 0)
                return id;

            if (controller.isGrounded)
                next = MOVE;

            return next;
        }
    }
}
