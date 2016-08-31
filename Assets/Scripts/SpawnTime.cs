using UnityEngine;
using System.Collections;

public class SpawnTime : MonoBehaviour {

    public float time;

    public void setTime(float spawnTime)
    {
        time = spawnTime;
    } 

    public float getSpawnTime()
    {
        return time;
    }
}
