using UnityEngine;

public class ChargingForwardBehavior : BeginEndBehavior
{
    public System.Action OnCompletedCharge;

    [Range(1, 100)]
    public int chargeSpeed;

    private bool isCharging;

    void Awake()
    {
        isCharging = false;
    }

    void Update()
    {
        if (isCharging)
        {
            transform.Translate(chargeSpeed * Time.deltaTime * Vector3.forward);
        }
    }

    public override void Begin()
    {
        isCharging = true;
    }

    public override void End()
    {
        isCharging = false;
        OnCompletedCharge?.Invoke();
    }
}
