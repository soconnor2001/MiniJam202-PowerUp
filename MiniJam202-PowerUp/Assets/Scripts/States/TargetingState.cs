using System;
using UnityEngine;

public class TargetingState : IState, IDisposable
{
    public Action OnTargetLocked;

    private readonly RotatingToTargetBehavior rotatingToTargetBehavior;

    public TargetingState(RotatingToTargetBehavior rotatingToTargetBehavior)
    {
        this.rotatingToTargetBehavior = rotatingToTargetBehavior;
        this.rotatingToTargetBehavior.OnCompletedRotation += HandleCompletedRotation;
    }

    public void Enter()
    {
        rotatingToTargetBehavior.Begin();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        rotatingToTargetBehavior.End();
    }

    public void Dispose()
    {
        rotatingToTargetBehavior.OnCompletedRotation -= HandleCompletedRotation;
    }

    private void HandleCompletedRotation()
    {
        OnTargetLocked?.Invoke();
    }
}
