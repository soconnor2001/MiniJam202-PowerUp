using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseMenuObj;

    public bool canPause = true;
    

    public void togglePauseMenu()
    {
        PauseMenuObj.SetActive(!PauseMenuObj.activeSelf);
        Time.timeScale = System.Convert.ToInt32(!PauseMenuObj.activeSelf);
    }

    public void returnToMenu()
    {
        //Debug.Log(SceneManager.GetSceneAt(1).name);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
        
    }

    public void reloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
