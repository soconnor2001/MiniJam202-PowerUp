using UnityEngine;

public class GameOverTimeSet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetTime(float Totalseconds)
    {
        int minutes = (int)(Totalseconds / 60);
        int seconds = (int)Totalseconds % 60;
        int miliseconds = (int)(Totalseconds-Mathf.Floor(Totalseconds));

        gameObject.GetComponent<TextMesh>().text = minutes.ToString() + ":" + seconds.ToString()+"."+miliseconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
