using System;
using UnityEngine;

public class PositioningState : IState, IDisposable
{
    public Action OnTargetReached;

    private readonly BobbingInAirBehavior bobbingInAirBehavior;
    private readonly MovingToTargetBehavior movingToTargetBehavior;

    public PositioningState(BobbingInAirBehavior bobbingInAirBehavior, MovingToTargetBehavior movingToTargetBehavior)
    {
        this.bobbingInAirBehavior = bobbingInAirBehavior;
        this.movingToTargetBehavior = movingToTargetBehavior;
        this.movingToTargetBehavior.OnCompletedMovement += HandleCompletedMovement;
    }

    public void Enter()
    {
        bobbingInAirBehavior.Begin();
        movingToTargetBehavior.Begin();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        bobbingInAirBehavior.End();
        movingToTargetBehavior.End();
    }

    public void Dispose()
    {
        movingToTargetBehavior.OnCompletedMovement -= HandleCompletedMovement;
    }

    private void HandleCompletedMovement()
    {
        OnTargetReached?.Invoke();
    }
}
