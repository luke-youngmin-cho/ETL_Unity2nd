using System.Collections.Generic;

namespace FSM.Generic
{
    public static class StateDataSheet

    {
        // Dictionary 가 아닌 IEnumerable 을 반환하는 이유는 
        // 소스 원본을 넘겼을때는 넘겨받은 측에서 원본을 수정할 수 있기 때문에
        // 순회할수있는 기능만 추상화하고있는 Enumerator 관련 인터페이스 혹은 읽기전용으로 바꿔서 넘겨야함.
        public static IEnumerable<KeyValuePair<CharacterState, IState<CharacterState>>> GetPlayerData(StateMachine<CharacterState> machine)
        {
            return new Dictionary<CharacterState, IState<CharacterState>>()
            {
                { CharacterState.Move, new Move(CharacterState.Move, machine) },
            };
        }
    }
}