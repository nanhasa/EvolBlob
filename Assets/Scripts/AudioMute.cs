using UnityEngine;
using System.Collections;

public class AudioMute : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (PlayerPrefs.GetString("AudioMute", "false") == "true") {
            GetComponent<AudioSource>().mute = true;
        }
        else if (PlayerPrefs.GetString("AudioMute", "false") == "false") {
            GetComponent<AudioSource>().mute = false;
        }
	}
}
