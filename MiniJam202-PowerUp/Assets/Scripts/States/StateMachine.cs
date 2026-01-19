using UnityEngine;

[DisallowMultipleComponent]
public class StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public void Update()
    {
        CurrentState?.Execute();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}
