using UnityEngine;

public class WaitingBehavior : BeginEndBehavior
{
    public System.Action OnCompletedWait;

    [Range(0.0f, 10.0f)]
    public float waitPeriod;

    private bool isWaiting;
    private float waitTimer;

    void Awake()
    {
        isWaiting = false;
        waitTimer = waitPeriod;
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                End();
            }
        }
    }

    public override void Begin()
    {
        isWaiting = true;
    }

    public override void End()
    {
        if (isWaiting)
        {
            isWaiting = false;
            waitTimer = waitPeriod;

            OnCompletedWait?.Invoke();
        }
    }
}
