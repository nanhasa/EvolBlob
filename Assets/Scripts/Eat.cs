using UnityEngine;
using System.Collections;

public class Eat : MonoBehaviour {

    public AudioClip eatableEaten;
    public AudioClip enemyEaten;

    private GameObject audioSystem;

    void Start() {
        audioSystem = GameObject.FindGameObjectWithTag("AudioSystem");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Eatable") {
            if (transform.tag == "Player")
                audioSystem.GetComponent<AudioManager>().playSound(eatableEaten, transform.position, 1.0f);
            GetComponent<Scale>().grow();
            coll.gameObject.SetActive(false);
        }
        if (coll.gameObject.tag == "Enemy") {
            if (transform.localScale.x > coll.gameObject.transform.localScale.x) {
                if (transform.tag == "Player")
                    audioSystem.GetComponent<AudioManager>().playSound(enemyEaten, transform.position, 1.0f);
                GetComponent<Scale>().grow(coll.gameObject.transform.localScale.x);
                //Set smaller object not active
                coll.gameObject.SetActive(false);
            }
        }
        if (coll.gameObject.tag == "Player")
            if (transform.localScale.x > coll.gameObject.transform.localScale.x) 
                //End scene
                coll.gameObject.GetComponent<GameEnd>().gameLost();
    }
}
