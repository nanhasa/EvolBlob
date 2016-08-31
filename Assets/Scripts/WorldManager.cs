using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType {ENEMY, EATABLE};

public class WorldManager : MonoBehaviour {

    //Limits
    public int enemies = 15;
    public int eatables = 40;

    //Player
    public GameObject player;

    //Container for gameObjects in the game (holds player, enemies and eatables)
    private List<GameObject> actorList;

	// Use this for initialization
	void Start () {

        actorList = new List<GameObject>();
        actorList.Add(player);

        int enemyCount = 0;
        int eatableCount = 0;

	    while (enemyCount < enemies) {
            actorList.Add(GetComponent<EnemySpawner>().spawnRandomActor(EnemyType.ENEMY));
            ++enemyCount;
        }
        while (eatableCount < eatables) {
            actorList.Add(GetComponent<EnemySpawner>().spawnRandomActor(EnemyType.EATABLE));
            ++eatableCount;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Loops the actorList to see which objects are not active and reassigns them to the game
        //which means new spot and new size
        for (int i = 0; i < actorList.Count; ++i) {
            if (actorList[i].tag == "Player") continue;
            if (!actorList[i].activeSelf)
                GetComponent<EnemySpawner>().reassignActor(actorList[i]);
        }
    }
}
