using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    PauseMenu pauseMenuObj;
    InputAction pauseAction;
    public bool canPause;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuObj = Instantiate(((GameObject)Resources.Load("Prefabs/PauseScreen"))).GetComponentInChildren<PauseMenu>();
        pauseMenuObj.canPause = canPause;
        pauseMenuObj.togglePauseMenu(); //has to start active for Resources.Load()
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseAction.WasPressedThisFrame() && pauseMenuObj.canPause)
        {
            pauseMenuObj.togglePauseMenu();
        }
    }
}
