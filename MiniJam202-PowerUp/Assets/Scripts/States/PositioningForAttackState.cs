using System;
using UnityEngine;

public class PositioningForAttackState : IState, IDisposable
{
    public Action OnTargetReached;

    private readonly BobbingInAirBehavior bobbingInAirBehavior;
    private readonly MovingToTargetBehavior movingToTargetBehavior;

    public PositioningForAttackState(BobbingInAirBehavior bobbingInAirBehavior, MovingToTargetBehavior movingToTargetBehavior)
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

    private void HandleCompletedMovement()
    {
        OnTargetReached?.Invoke();
    }

    public void Dispose()
    {
        movingToTargetBehavior.OnCompletedMovement -= HandleCompletedMovement;
    }
}
