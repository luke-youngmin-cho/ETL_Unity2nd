using System;

namespace FSM.Generic
{
    public interface IState<T>
        where T : Enum
    {
        T id { get; }
        bool canExecute { get; }
        void OnEnter();
        void OnExit();
        T OnUpdate();
        void OnFixedUpdate();
    }
}