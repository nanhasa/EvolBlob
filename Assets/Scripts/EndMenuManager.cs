using UnityEngine;
using System.Collections;
using System;

public class EndMenuManager : MonoBehaviour {

    public void retry() {
        Debug.Log("RETRY clicked");
        string sceneName = PlayerPrefs.GetString("LastLevel", "");
        if (sceneName != "") {
            Application.LoadLevel(sceneName);
        }
        else {
            Debug.Log("ERROR: No last level found");
        }
        
    }

    public void nextLevel() {
        Debug.Log("NEXT LEVEL clicked");
        string sceneName = PlayerPrefs.GetString("LastLevel", "");
        if (sceneName != "") {
            int nextLevel;
            if (Int32.TryParse(sceneName[sceneName.Length - 1].ToString(), out nextLevel)) {
                ++nextLevel;
                if (nextLevel > PlayerPrefs.GetInt("NumberOfLevels", 1))
                    Application.LoadLevel("scene" + PlayerPrefs.GetInt("NumberOfLevels", 1));
                else { 
                    Application.LoadLevel("scene" + nextLevel.ToString());
                }
            }
            else {
                Debug.Log("ERROR: Scene name didn't end in a number");
            }
        } else {
            Debug.Log("ERROR: No last level found");
        }
        
    }

    public void highscore() {
        Debug.Log("HIGHSCORE clicked");
    }

    public void buy() {
        Debug.Log("BUY clicked");
    }

    public void mainMenu() {
        Debug.Log("MAIN MENU clicked");
        Application.LoadLevel("MainMenuScene");
    }
}
