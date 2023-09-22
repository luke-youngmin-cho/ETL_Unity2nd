public interface IState
{
    bool canExecute { get; }
    void OnEnter();
    void OnExit();
    int OnUpdate();
    void OnFixedUpdate();
}
