using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LevelSelection : MonoBehaviour {

    public Sprite open;
    public Sprite closed;
    public Sprite audioOn;
    public Sprite audioOff;
    public AudioClip levelOpenAudio;
    public AudioClip levelUnopenAudio;

    public int numberOfLevels = 4;

    private GameObject audioSystem;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("NumberOfLevels", numberOfLevels);

        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem");
        int levels = PlayerPrefs.GetInt("LevelsOpen", 1);

        Component[] textComp = GetComponentsInChildren<Text>();
        foreach (Text child in textComp) {
            if (child.tag == "LevelText") {
                int textNum = 0;

                if (Int32.TryParse(child.text, out textNum)) {
                    if (levels >= textNum) {
                        child.transform.parent.GetComponent<Image>().sprite = open;
                    }else {
                        child.transform.parent.GetComponent<Image>().sprite = closed;
                    }
                }else {
                    Debug.Log("ERROR: Level button text was other than number: " + child.text);
                }
            }
        }

        GameObject audioButton = GameObject.FindGameObjectWithTag("MuteButton");
        if (PlayerPrefs.GetString("AudioMute", "false") == "false") 
            audioButton.GetComponent<Image>().sprite = audioOn;
        else if (PlayerPrefs.GetString("AudioMute", "false") == "true") 
            audioButton.GetComponent<Image>().sprite = audioOff;
    }
	
	// Update is called once per frame
	void Update () {
        int levels = PlayerPrefs.GetInt("LevelsOpen", 1);

        Component[] textComp = GetComponentsInChildren<Text>();
        foreach (Text child in textComp) {
            if (child.tag == "LevelText") {
                int textNum = 0;

                if (Int32.TryParse(child.text, out textNum)) {
                    if (levels >= textNum) {
                        child.transform.parent.GetComponent<Image>().sprite = open;
                    }
                    else {
                        child.transform.parent.GetComponent<Image>().sprite = closed;
                    }
                }
                else {
                    Debug.Log("ERROR: Level button text was other than number: " + child.text);
                }
            }
        }
    }

    public void selectLevel(int level) {
        Debug.Log("Level " + level.ToString() + " selected");

        if (PlayerPrefs.GetInt("LevelsOpen", 1) >= level) {
            audioSystem.GetComponent<AudioManager>().playSound(levelOpenAudio, transform.position, 1.0f);
            Application.LoadLevel("scene" + level.ToString());
        }
        else {
            audioSystem.GetComponent<AudioManager>().playSound(levelUnopenAudio, transform.position, 1.0f);
            Debug.Log("Chosen level is not yet opened");
        }
            
    }

    public void toggleAudio() {
        GameObject audioButton = GameObject.FindGameObjectWithTag("MuteButton");
        if (PlayerPrefs.GetString("AudioMute", "false") == "false") {
            PlayerPrefs.SetString("AudioMute", "true");
            audioButton.GetComponent<Image>().sprite = audioOff;

        } else if (PlayerPrefs.GetString("AudioMute", "false") == "true") {
            PlayerPrefs.SetString("AudioMute", "false");
            audioButton.GetComponent<Image>().sprite = audioOn;
        }
    }
}
