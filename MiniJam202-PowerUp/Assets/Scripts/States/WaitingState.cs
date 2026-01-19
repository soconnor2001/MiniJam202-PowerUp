using System;
using UnityEngine;

public class WaitingState : IState, IDisposable
{
    public Action OnDoneWaiting;

    private readonly WaitingBehavior waitingBehavior;

    public WaitingState(WaitingBehavior waitingBehavior)
    {
        this.waitingBehavior = waitingBehavior;
        this.waitingBehavior.OnCompletedWait += HandleCompletedWait;
    }

    public void Enter()
    {
        waitingBehavior.Begin();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        waitingBehavior.End();
    }

    public void Dispose()
    {
        waitingBehavior.OnCompletedWait -= HandleCompletedWait;
    }

    private void HandleCompletedWait()
    {
        OnDoneWaiting?.Invoke();
    }
}
