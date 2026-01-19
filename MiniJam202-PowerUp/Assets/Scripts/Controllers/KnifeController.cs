using UnityEngine;

[DisallowMultipleComponent]
public class KnifeController : MonoBehaviour
{
    public StateMachine stateMachine;

    public BobbingInAirBehavior bobbingInAirBehavior;
    public MovingToTargetBehavior movingToTargetBehavior;
    public RotatingToTargetBehavior rotatingToTargetBehavior;
    public WaitingBehavior windupBehavior;
    public ChargingForwardBehavior chargingForwardBehavior;
    public WaitingBehavior cooldownBehavior;

    private PositioningState positioningForAttackState;
    private TargetingState targetingPlayerState;
    private WaitingState windupState;
    private ChargingState chargingState;
    private WaitingState cooldownState;

    void Start()
    {
        positioningForAttackState = new PositioningState(bobbingInAirBehavior, movingToTargetBehavior);
        positioningForAttackState.OnTargetReached += HandleTargetReached;

        targetingPlayerState = new TargetingState(rotatingToTargetBehavior);
        targetingPlayerState.OnTargetLocked += HandleTargetLocked;

        windupState = new WaitingState(windupBehavior);
        windupState.OnDoneWaiting += HandleWindupDone;

        chargingState = new ChargingState(chargingForwardBehavior);
        chargingState.OnDoneCharging += HandleChargeDone;

        cooldownState = new WaitingState(cooldownBehavior);
        cooldownState.OnDoneWaiting += HandleCooldownDone;

        stateMachine.ChangeState(positioningForAttackState);
    }

    void OnDestroy()
    {
        positioningForAttackState.OnTargetReached -= HandleTargetReached;
        targetingPlayerState.OnTargetLocked -= HandleTargetLocked;
        windupState.OnDoneWaiting -= HandleWindupDone;
        chargingState.OnDoneCharging -= HandleChargeDone;
        cooldownState.OnDoneWaiting -= HandleCooldownDone;
    }

    public void AbortChargeOnEnvironmentCollision()
    {
        if (stateMachine.CurrentState == chargingState)
        {
            stateMachine.ChangeState(cooldownState);
        }
    }

    private void HandleTargetReached()
    {
        stateMachine.ChangeState(targetingPlayerState);
    }

    private void HandleTargetLocked()
    {
        stateMachine.ChangeState(windupState);
    }

    private void HandleWindupDone()
    {
        stateMachine.ChangeState(chargingState);
    }

    private void HandleChargeDone()
    {
        stateMachine.ChangeState(cooldownState);
    }

    private void HandleCooldownDone()
    {
        stateMachine.ChangeState(positioningForAttackState);
    }
}
