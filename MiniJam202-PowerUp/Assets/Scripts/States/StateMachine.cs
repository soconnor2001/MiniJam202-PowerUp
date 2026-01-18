using UnityEngine;

[DisallowMultipleComponent]
public class StateMachine : MonoBehaviour
{
    private IState currentState;

    public void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
