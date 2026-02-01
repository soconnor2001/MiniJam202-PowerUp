using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] AudioSource buttonClick;

    public void playButton()
    {
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    
}
