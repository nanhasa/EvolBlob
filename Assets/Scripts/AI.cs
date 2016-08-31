using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum AIState {PATROL, ATTACK, RUN};

public class AI : MonoBehaviour {

    public float moveForce = 20f;
    private float currentSpeedLimit = 4.5f;
    public float weightSpeedFactor = 3f;
    public AIState state;

    public float attackSpeedLimit = 3.7f;
    public float normalSpeedLimit = 4.5f;

    public GameObject border;

    private List<GameObject> nearEnemiesList;

    // Use this for initialization
    void Start () {
        nearEnemiesList = new List<GameObject>();
        GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * moveForce);
        state = AIState.PATROL;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (nearEnemiesList.Count == 0) state = AIState.PATROL;

        switch (state) {
            case AIState.PATROL:
                currentSpeedLimit = normalSpeedLimit - weigthSlowing();
                randomizeMovement();
                break;
            case AIState.ATTACK:
                currentSpeedLimit = attackSpeedLimit - weigthSlowing();
                attack();
                break;
            case AIState.RUN:
                currentSpeedLimit = normalSpeedLimit - weigthSlowing();
                runFromTarget();
                break;
            default:
                Debug.Log("ERROR: Undefined state in AI/FixedUpdate: " + state.ToString());
                break;
        }
        restrictVelocity();
        nearEnemiesList.Clear();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //Leaves out border, eatables and search_radius_collider
        if (col.transform.parent == null)
        {
            if (col.tag == "Player" || col.tag == "Enemy")
            {
                //Add new enemy to near enemy list if it is not there yet
                nearEnemiesList.Add(col.gameObject);
                float enemyScale = col.gameObject.transform.localScale.x;
                if (enemyScale > transform.localScale.x) state = AIState.RUN;
                else if (state != AIState.RUN && enemyScale < transform.localScale.x) state = AIState.ATTACK;
            }
        }
    }

    //Nothing interesting close by so randomize movement
    private void randomizeMovement() {
        int random = UnityEngine.Random.Range(0, 100);
        //Randomize new direction
        if (random > 70) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * moveForce);
        }else {
            //Keep adding force for the existing direction
            GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity.normalized * moveForce);
        }
    }

    private void attack() {
        float closest = -1; //initial value to avoid error
        GameObject target = null;

        //Find the closest enemy
        for (int i = 0; i < nearEnemiesList.Count; ++i) {
            if (i == 0) {
                target = nearEnemiesList[i];
                closest = Vector2.Distance(transform.position, nearEnemiesList[i].transform.position);
            }
            if (closest > Vector2.Distance(transform.position, nearEnemiesList[i].transform.position)) { 
                closest = Vector2.Distance(transform.position, nearEnemiesList[i].transform.position);
            }
        }
        GetComponent<Rigidbody2D>().AddForce((target.gameObject.transform.position - transform.position).normalized * moveForce);
    }

    private void runFromTarget() {
        //Calculate mass center of enemies to run away from
        Vector2 sumOfPositions = Vector2.zero;
        for (int i = 0; i < nearEnemiesList.Count; ++i) {
            if (nearEnemiesList[i].transform.localScale.x > transform.localScale.x) {
                sumOfPositions.x += nearEnemiesList[i].transform.position.x;
                sumOfPositions.y += nearEnemiesList[i].transform.position.y;
            }
        }
        //check that there were some enemies to be sure
        if (nearEnemiesList.Count > 0)
        {
            sumOfPositions.x /= nearEnemiesList.Count;
            sumOfPositions.y /= nearEnemiesList.Count;
            //We want opposite direction of the calculated mass point of enemies
            GetComponent<Rigidbody2D>().AddForce(-sumOfPositions.normalized * moveForce);
        }else {
            Debug.Log("ERROR: Enemy tries to run when there's no targets to run from.");
        }
    }
    
    private void restrictVelocity() {
        float velocityLength = Mathf.Sqrt(Mathf.Pow(GetComponent<Rigidbody2D>().velocity.x, 2f) + Mathf.Pow(GetComponent<Rigidbody2D>().velocity.y, 2f));
        if (velocityLength > currentSpeedLimit) {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * currentSpeedLimit;
        }
    }

    private float weigthSlowing() {
        return transform.localScale.x / weightSpeedFactor;
    }
}
