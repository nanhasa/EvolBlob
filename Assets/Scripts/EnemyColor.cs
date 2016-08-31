using UnityEngine;
using System.Collections;

public class EnemyColor : MonoBehaviour {

    GameObject player;

    public Sprite smaller;
    public Sprite bigger;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        float scale = transform.parent.localScale.x;
        if (scale < player.transform.localScale.x) { //Bigger enemy sprite is default so no need for it here
            GetComponent<SpriteRenderer>().sprite = smaller;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float scale = transform.parent.localScale.x;
        if (scale < player.transform.localScale.x) {
            GetComponent<SpriteRenderer>().sprite = smaller;
        }else {
            GetComponent<SpriteRenderer>().sprite = bigger;
        }
    }
}
