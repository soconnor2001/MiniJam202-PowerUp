using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [Range(1, 50)]
    public int moveSpeed;

    public CharacterController controller;

    public Health health;

    public bool Immortal = false;

    private InputAction moveAction;
    private float initialYPosition;
    [HideInInspector]
    public bool isAlive;

    void Awake()
    {
        if (health == null)
        {
            Debug.LogError("Health component is not configured.");
        }

        moveAction = InputSystem.actions.FindAction("Move");
        initialYPosition = transform.position.y;
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
                Vector3 translationToApply = moveSpeed * Time.deltaTime * new Vector3(moveValue.x, 0, moveValue.y);
                controller.Move(translationToApply);
                transform.position = new(transform.position.x, initialYPosition, transform.position.z);
            }
        }
    }

    public void Die()
    {
        if(!Immortal)
        {
            Debug.Log("RANA HAS FALLEN :(");
        }
        
    }
}
