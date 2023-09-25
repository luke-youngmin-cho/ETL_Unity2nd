namespace FSM
{
    public interface IState 
    { 
        int id { get; }
        bool canExecute { get; }
        void OnEnter();
        void OnExit();
        int OnUpdate();
        void OnFixedUpdate();
    }
}