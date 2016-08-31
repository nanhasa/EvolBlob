using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public void playSound(AudioClip sound, Vector3 position, float volume = 1.0f) {
        if (GetComponent<AudioSource>().mute == false) {
            AudioSource.PlayClipAtPoint(sound, position, volume);
        }
    }
}
