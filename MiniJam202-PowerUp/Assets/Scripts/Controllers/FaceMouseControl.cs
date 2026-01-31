using UnityEngine;
using UnityEngine.InputSystem;

public class FaceMouseControl : MonoBehaviour
{
    public PlayerController playerControllerScript;

    private InputAction MouseAimAction;

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        MouseAimAction = InputSystem.actions.FindAction("MouseAim");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isAlive)
        {
            var mousePos = MouseAimAction.ReadValue<Vector2>();
            var screenToWorldRay = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(screenToWorldRay, out var hit, 1000, LayerMask.GetMask("Floor")))
            {
                transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
                transform.Rotate(Vector3.up,-90);
            }

        }
    }
}
