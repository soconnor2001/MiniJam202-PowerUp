using UnityEngine;

public class RotatingToTargetBehavior : BeginEndBehavior
{
    public System.Action OnCompletedRotation;

    public Transform target;

    [Range(10, 500)]
    public int rotationSpeed;

    [Range(0.1f, 10.0f)]
    public float rotationPeriod;

    [Range(0.0f, 45.0f)]
    public float inaccuracy;

    private Vector3 rotationDirection;
    private bool isRotating;
    private float rotationTimer;

    void Awake()
    {
        if (target == null)
        {
            Debug.LogError("Target to face is not configured.");
        }

        rotationDirection = new Vector3(0, 1, 0);
        isRotating = false;
        rotationTimer = rotationPeriod;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime * rotationDirection);

            rotationTimer -= Time.deltaTime;
            if (rotationTimer <= 0)
            {
                End();
            }
        }
    }

    public override void Begin()
    {
        isRotating = true;
    }

    public override void End()
    {
        if (isRotating)
        {
            float randomInaccuracy = inaccuracy > 0 ? Random.Range(-inaccuracy, inaccuracy) : 0;

            isRotating = false;
            rotationTimer = rotationPeriod;

            Vector3 targetWithYAxisMatchedToSelf = new(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetWithYAxisMatchedToSelf);

            OnCompletedRotation?.Invoke();
        }
    }
}
