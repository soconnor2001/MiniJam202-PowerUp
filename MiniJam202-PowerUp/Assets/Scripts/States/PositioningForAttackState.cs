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
        this.movingToTargetBehavior.OnArrived += HandleArrived;
    }

    public void Enter()
    {
        bobbingInAirBehavior.StartBobbing();
        movingToTargetBehavior.StartMoving();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        bobbingInAirBehavior.StopBobbing();
        movingToTargetBehavior.StopMoving();
        movingToTargetBehavior.InitializeTargetInfo();
    }

    private void HandleArrived()
    {
        OnTargetReached?.Invoke();
    }

    public void Dispose()
    {
        movingToTargetBehavior.OnArrived -= HandleArrived;
    }

}
