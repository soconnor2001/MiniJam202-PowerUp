using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [Range(1f, 50f)]
    public float speed;
    public Health health;

    private InputAction moveAction;
    private bool isAlive;

    void Start()
    {
        if (health == null)
        {
            Debug.LogError("Health component is not configured.");
        }

        moveAction = InputSystem.actions.FindAction("Move");
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (moveAction.IsPressed())
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if (moveValue != null)
            {
                Vector3 translationToApply = speed * Time.deltaTime * new Vector3(moveValue.x, 0, moveValue.y);
                transform.Translate(translationToApply);
            }
        }
    }

}
