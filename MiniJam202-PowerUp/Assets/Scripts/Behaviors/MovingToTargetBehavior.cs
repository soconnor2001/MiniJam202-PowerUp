using UnityEngine;

public class MovingToTargetBehavior : BeginEndBehavior
{
    const int xBound = 9;
    const int zBound = 9;

    public System.Action OnCompletedMovement;

    [Range(1, 50)]
    public int moveSpeed;

    [Tooltip("The target to move to.")]
    public Transform moveTarget;

    [Tooltip("If true, select a random target point within the bounding box.")]
    public bool useRandomMoveTarget;

    [Tooltip("The target to face while moving. If unspecified, face towards the move target.")]
    public Transform faceTarget;

    private Vector3 moveTargetVector;
    private Vector3 faceTargetVector;
    private Vector3 direction;
    private bool isMoving;
    private bool arrivedAtTarget;

    void Update()
    {
        if (isMoving && !arrivedAtTarget)
        {
            if (CalculateRemainingDistance() < 0.1)
            {
                arrivedAtTarget = true;
                OnCompletedMovement?.Invoke();
            }
            else
            {
                transform.position += moveSpeed * Time.deltaTime * direction;
                transform.LookAt(faceTargetVector);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            }
        }
    }

    public override void Begin()
    {
        InitializeTargetInfo();
        isMoving = true;
    }

    public override void End()
    {
        isMoving = false;
    }

    public void InitializeTargetInfo()
    {
        moveTargetVector = useRandomMoveTarget ? CalculateRandomMoveTarget() : moveTarget.position;
        faceTargetVector = faceTarget == null ? moveTargetVector : faceTarget.position;
        direction = CalculateHeading().normalized;
        arrivedAtTarget = false;
    }

    private Vector3 CalculateRandomMoveTarget()
    {
        return new Vector3(Random.Range(-xBound, xBound), 0, Random.Range(-zBound, zBound));
    }

    private Vector3 CalculateHeading()
    {
        Vector3 heading = moveTargetVector - transform.position;
        heading.y = 0;
        return heading;
    }

    private float CalculateRemainingDistance()
    {
        Vector2 self = new(transform.position.x, transform.position.z);
        Vector2 target = new(moveTargetVector.x, moveTargetVector.z);
        return Vector2.Distance(self, target);
    }
}
