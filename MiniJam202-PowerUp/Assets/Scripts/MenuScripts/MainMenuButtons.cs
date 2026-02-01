using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] AudioSource buttonClick;
    [SerializeField] AudioSource buttonClickBack;

    public void playButton()
    {
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void forwardButton()
    {
        buttonClick.Play();
    }
    
    public void backButton()
    {
        buttonClickBack.Play();
    }

    public void quitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    
}
