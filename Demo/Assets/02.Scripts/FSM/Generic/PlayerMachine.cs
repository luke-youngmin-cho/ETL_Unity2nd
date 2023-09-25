using UnityEngine;

namespace FSM.Generic
{
    public class PlayerMachine : StateMachine<CharacterState>
    {
        public PlayerMachine(GameObject owner) : base(owner)
        {
            Initialize(StateDataSheet.GetPlayerData(this));
        }
    }
}