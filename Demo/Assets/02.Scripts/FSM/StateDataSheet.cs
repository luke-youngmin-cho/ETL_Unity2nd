using System.Collections.Generic;

namespace FSM
{
    public static class StateDataSheet
    {
        public static IEnumerable<KeyValuePair<int, IState>> GetPlayerData(StateMachine machine)
        {
            return new Dictionary<int, IState>()
        {
            { StateBase.MOVE, new Move(StateBase.MOVE, machine) },
            { StateBase.JUMP, new Jump(StateBase.JUMP, machine, 5.0f) },
            { StateBase.FALL, new Fall(StateBase.FALL, machine) },
        };
        }
    }
}