namespace FSM.Generic
{
    public class Move : CharacterStateBase
    {
        public Move(CharacterState id, StateMachine<CharacterState> machine) : base(id, machine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            // todo -> play animation...
            // todo -> unlock movement...
        }

        public override CharacterState OnUpdate()
        {
            CharacterState next = base.OnUpdate();

            // todo -> if not grounded, next to fall

            return next;
        }
    }
}