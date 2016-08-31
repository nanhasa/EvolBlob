using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnParticles : MonoBehaviour {

    public GameObject particle;
    public float timeInterval = 0.2f;
    public float timeSpan = 0.6f;
    public float force = 1f;

    private GameObject timer;
    private float lastSpawn = 0;
    private List<GameObject> particleList;

    void Start() {
        timer = GameObject.Find("Timer");
        particleList = new List<GameObject>();
    }

    void FixedUpdate() {
        for (int i = particleList.Count - 1; i >= 0; --i) {
            particleList[i].transform.localScale -= new Vector3(0.05f, 0.05f, 0.5f);
            if (timer.GetComponent<Timer>().getTimeFromStart() - particleList[i].GetComponent<SpawnTime>().getSpawnTime() >= timeSpan) {
                GameObject delete = particleList[i];
                particleList.RemoveAt(i);
                Destroy(delete);
            }
        }
    }

    public void spawnParticle() {
        float timeStamp = timer.GetComponent<Timer>().getTimeFromStart();
        if (timeStamp - lastSpawn >= timeInterval) {
            float radius = GetComponent<CircleCollider2D>().radius;
            float scale = transform.localScale.x;
            GameObject spawn = (GameObject)Instantiate(particle, 
                transform.position - (radius * scale * new Vector3(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y, 0).normalized), 
                transform.rotation);
            spawn.GetComponent<Rigidbody2D>().AddForce(-GetComponent<Rigidbody2D>().velocity * force);
            spawn.GetComponent<SpawnTime>().setTime(timeStamp);
            particleList.Add(spawn);
            lastSpawn = timeStamp;
        }
    }
}
