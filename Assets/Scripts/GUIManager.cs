using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public bool isPaused;

    //timer
    private GameObject timer;

    void Start() {
        isPaused = false;
        timer = GameObject.Find("Timer");
    }

	// Update is called once per frame
	void Update () {
        //Time
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = timer.GetComponent<Timer>().getTimeFromStart().ToString();
    }

    public void pause() {
        Debug.Log("Pause clicked");
        if (isPaused == false) 
            //Slow down time to "still"
            Time.timeScale = 0.0000001f;
        else 
            //Make timescale back to normal
            Time.timeScale = 1f;
        timer.GetComponent<Timer>().toggleClockPause();
        toggleIsPaused();
    }

    private void toggleIsPaused() {
        isPaused = !isPaused;
    }
}