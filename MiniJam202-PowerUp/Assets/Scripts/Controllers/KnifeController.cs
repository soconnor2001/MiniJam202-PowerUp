using UnityEngine;

[DisallowMultipleComponent]
public class KnifeController : MonoBehaviour
{
    public StateMachine stateMachine;

    public BobbingInAirBehavior bobbingInAirBehavior;
    public MovingToTargetBehavior movingToTargetBehavior;
    public RotatingToTargetBehavior rotatingToTargetBehavior;

    private PositioningForAttackState positioningForAttackState;
    private TargetingPlayerState targetingPlayerState;

    void Start()
    {
        positioningForAttackState = new PositioningForAttackState(bobbingInAirBehavior, movingToTargetBehavior);
        positioningForAttackState.OnTargetReached += HandleTargetReached;

        targetingPlayerState = new TargetingPlayerState(rotatingToTargetBehavior);
        targetingPlayerState.OnTargetLocked += HandleTargetLocked;

        stateMachine.ChangeState(positioningForAttackState);
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        positioningForAttackState.OnTargetReached -= HandleTargetReached;
        targetingPlayerState.OnTargetLocked -= HandleTargetLocked;
    }

    private void HandleTargetReached()
    {
        stateMachine.ChangeState(targetingPlayerState);
    }

    private void HandleTargetLocked()
    {
        stateMachine.ChangeState(positioningForAttackState);
    }

}
