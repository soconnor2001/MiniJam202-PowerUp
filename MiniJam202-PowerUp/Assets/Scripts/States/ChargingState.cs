using System;
using UnityEngine;

public class ChargingState : IState, IDisposable
{
    public Action OnDoneCharging;

    private readonly ChargingForwardBehavior chargingForwardBehavior;

    public ChargingState(ChargingForwardBehavior chargingForwardBehavior)
    {
        this.chargingForwardBehavior = chargingForwardBehavior;
        this.chargingForwardBehavior.OnCompletedCharge += HandleCompletedCharge;
    }

    public void Enter()
    {
        chargingForwardBehavior.Begin();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        chargingForwardBehavior.End();
    }

    public void Dispose()
    {
        chargingForwardBehavior.OnCompletedCharge -= HandleCompletedCharge;
    }

    private void HandleCompletedCharge()
    {
        OnDoneCharging?.Invoke();
    }
}
