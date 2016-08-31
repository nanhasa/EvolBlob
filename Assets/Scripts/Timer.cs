using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    private float timeFromStart = 0;
    private bool isPaused = false;

    void Update()
    {
        if (!isPaused)
            timeFromStart += Time.deltaTime;
    }

    public void toggleClockPause()
    {
        isPaused = !isPaused;
        Debug.Log("Clock is paused " + isPaused);
    }

    public float getTimeFromStart()
    {
        return timeFromStart;
    }
}
