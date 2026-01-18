using System;
using UnityEngine;

public class BobbingInAirBehavior : MonoBehaviour
{
    [Range(1f, 25.0f)]
    public float frequency;

    [Range(0.1f, 1.0f)]
    public float amplitude;

    private Vector3 startPosition;
    private bool isBobbing;
    private bool stopBobbing;

    void Start()
    {
        startPosition = transform.position;
        isBobbing = true;
        stopBobbing = false;
    }

    void Update()
    {
        if (isBobbing)
        {
            float vertical = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x, vertical, transform.position.z);
        }

        if (stopBobbing && Math.Abs(transform.position.y - startPosition.y) <= 0.01f)
        {
            ResetYAxis();
            isBobbing = false;
            stopBobbing = false;
        }
    }

    public void StartBobbing()
    {
        ResetYAxis();
        isBobbing = true;
        stopBobbing = false;
    }

    public void StopBobbing()
    {
        stopBobbing = true;
    }

    private void ResetYAxis()
    {
        transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
    }

}
