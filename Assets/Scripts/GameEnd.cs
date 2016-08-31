using UnityEngine;
using System.Collections;

public class GameEnd : MonoBehaviour {

    public float winScale = 4f;

    public void gameLost() {
        Debug.Log("Game ended");
        PlayerPrefs.SetString("LastLevel", Application.loadedLevelName);
        Application.LoadLevel("GameLostScene");
    }

    void Update() {
        if (transform.localScale.x >= winScale) {
            Debug.Log("Game won");
            int highestLevel = PlayerPrefs.GetInt("LevelsOpen", 1);
            Debug.Log("loaded level: " + Application.loadedLevel + " highest so far: " + highestLevel);
            if (Application.loadedLevel >= highestLevel)
                PlayerPrefs.SetInt("LevelsOpen", highestLevel + 1);
            Debug.Log("Highest level after winning: " + highestLevel);
            PlayerPrefs.SetString("LastLevel", Application.loadedLevelName);
            Application.LoadLevel("GameWonScene");
        }
    }
}
