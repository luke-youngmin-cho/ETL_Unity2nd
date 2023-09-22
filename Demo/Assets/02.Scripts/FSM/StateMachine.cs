using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public int current;
    public Dictionary<int, IState> states;
    private bool _isDirty;


    public bool ChangeState(int newID)
    {
        if (_isDirty)
            return false;

        if (newID == current)
            return false;

        if (states[newID].canExecute == false)
            return false;

        states[current].OnExit();
        current = newID;
        states[current].OnEnter();
        _isDirty = true;
        return true;
    }

    private void Initialize(Dictionary<int, IState> copy)
    {
        this.states = new Dictionary<int, IState>(copy);
    }

    private void Update()
    {
        ChangeState(states[current].OnUpdate());
    }

    private void FixedUpdate()
    {
        states[current].OnFixedUpdate();
    }

    private void LateUpdate()
    {
        _isDirty = false;
    }
}