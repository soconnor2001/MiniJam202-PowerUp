using System;
using UnityEngine;

public class TargetingPlayerState : IState, IDisposable
{
    public Action OnTargetLocked;

    private readonly RotatingToTargetBehavior rotatingToTargetBehavior;

    public TargetingPlayerState(RotatingToTargetBehavior rotatingToTargetBehavior)
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

    }

    private void HandleCompletedRotation()
    {
        OnTargetLocked?.Invoke();
    }

    public void Dispose()
    {
        rotatingToTargetBehavior.OnCompletedRotation -= HandleCompletedRotation;
    }
}
