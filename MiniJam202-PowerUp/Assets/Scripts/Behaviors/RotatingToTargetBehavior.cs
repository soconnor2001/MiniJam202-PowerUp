using UnityEngine;

public class RotatingToTargetBehavior : MonoBehaviour
{
    public System.Action OnCompletedRotation;

    public Transform target;

    [Range(10, 500)]
    public int rotationSpeed;

    [Range(0.1f, 10.0f)]
    public float rotationPeriod;

    [Range(0.0f, 1.0f)]
    public float inaccuracy;

    private Vector3 rotationDirection;
    private bool isRotating;
    private float rotationTimer;

    void Start()
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
                StopRotation();
                rotationTimer = rotationPeriod;
            }
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        float randomInaccuracy = inaccuracy > 0 ? Random.Range(-inaccuracy, inaccuracy) : 0;

        isRotating = false;
        transform.LookAt(target);
        transform.rotation = new Quaternion(0, transform.rotation.y + randomInaccuracy, 0, transform.rotation.w);

        OnCompletedRotation?.Invoke();
    }

}
