using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseMenuObj;
    public GameManager manager;

    public bool canPause = true;

    public void Start()
    {
        //manager = FindFirstObjectByType<GameManager>();
        //manager = GetComponent<GameManager>();
    }

    public void togglePauseMenu()
    {
        manager ??= FindAnyObjectByType<GameManager>();
        PauseMenuObj.SetActive(!PauseMenuObj.activeSelf);
        if(PauseMenuObj.activeSelf){
            //pause game music
            manager.themeMusic.Pause();
        }else manager.themeMusic.Play();//*/
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
