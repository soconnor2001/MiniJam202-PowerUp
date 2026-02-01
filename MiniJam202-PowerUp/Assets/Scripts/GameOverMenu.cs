using UnityEngine;
using TMPro;

public class GameOverMenu : PauseMenu
{
    public TextMeshProUGUI TimeText;
    public void SetTime(float time)
    {
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;
        int miliseconds = (int) time-(60*minutes) / 60;
        TimeText.text = minutes + ":" + seconds + "." + miliseconds;
    }
    
}
