using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject eatablePrefab;
    public GameObject border;

    private Camera cam;

    //[0] holds smallest value, [1] holds biggest value
    private float[] xVariance;
    private float[] yVariance;

    void Awake() {
        cam = Camera.main;

        xVariance = new float[2];
        yVariance = new float[2];

        //Find +x and +y
        bool found = true;
        for (float i = 0f; found; ++i)
        {
            found = false;
            //x-axis
            if (!border.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(i, 0f))) {
                xVariance[1] = i;
                found = true;
            }
            //y-axis
            if (!border.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(0f, i))) {
                yVariance[1] = i;
                found = true;
            }
        }
        //find -x and -y
        found = true;
        for (float i = 0f; found; --i) {
            found = false;
            //x-axis
            if (!border.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(i, 0f)))
            {
                xVariance[0] = i;
                found = true;
            }
            //y-axis
            if (!border.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(0f, i)))
            {
                yVariance[0] = i;
                found = true;
            }
        }

        Debug.Log("Smallest border x: " + xVariance[0] + " Biggest x: " + xVariance[1]);
        Debug.Log("Smallest border y: " + yVariance[0] + " Biggest y: " + yVariance[1]);
    }

    public GameObject spawnRandomActor(EnemyType type) {
        GameObject actor = null;
        if (type == EnemyType.ENEMY)
        {
            actor = (GameObject)Instantiate(enemyPrefab, getValidSpawnPoint(), Quaternion.identity);
            actor.GetComponent<Scale>().randomizeScale();
        }
        else if (type == EnemyType.EATABLE) {
            actor = (GameObject)Instantiate(eatablePrefab, getValidSpawnPoint(), Quaternion.identity);
        }
        else {
            Debug.Log("ERROR: Unknown tag in spawnRandomEnemy: " + type.ToString());
            return null;
        }
        return actor;
    }

    public void reassignActor(GameObject actor) {
        if (actor.tag != "Enemy" && actor.tag != "Eatable") {
            Debug.Log("ERROR: Unknown tag in reassignActor: " + tag);
            return;
        }
        actor.transform.position = getValidSpawnPoint();
        //Has to be set active before randomizing scale because child objects cannot be found if not active
        actor.SetActive(true);
        if (actor.tag == "Enemy") 
            actor.GetComponent<Scale>().randomizeScale();
    }

    private Vector3 getValidSpawnPoint() {
        //Calculate what is seen by camera
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Vector3 upRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        while (true) {
            //Randomize spawn point (x, y)
            float randomX = Random.Range(xVariance[0], xVariance[1]);
            float randomY = Random.Range(yVariance[0], yVariance[1]);

            //Point has to be outside camera but inside borders
            if (randomX > bottomLeft.x && randomX < upRight.x) {
                if (randomY > bottomLeft.y && randomY < upRight.y) continue;
            }
            if (border.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(randomX, randomY))) continue;

            return new Vector3(randomX, randomY, 0f);
        }
    }


}
