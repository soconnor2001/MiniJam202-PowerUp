using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class DeathScreenControl : MonoBehaviour
{
    GameOverMenu pauseMenuObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuObj = Instantiate(((GameObject)Resources.Load("Prefabs/GameOverScreen")))
            .GetComponentInChildren<GameOverMenu>();
        pauseMenuObj.togglePauseMenu(); //has to start active for Resources.Load()
    }

    public void Die()
    {
        pauseMenuObj.SetTime(Time.time);
        pauseMenuObj.togglePauseMenu();
    }
    
}
