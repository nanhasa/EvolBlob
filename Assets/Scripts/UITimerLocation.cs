using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITimerLocation : MonoBehaviour {

    private float timePositionFromRight = 50f;
    private float timePositionFromUp = 50f;
    //private Camera cam;

    // Use this for initialization
    void Start () {
        Camera cam = Camera.main;
        transform.position = new Vector3(cam.pixelWidth - timePositionFromRight, cam.pixelHeight - timePositionFromUp, 0f);
    }
}
