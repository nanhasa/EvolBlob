using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float speedLimit = 5f;
    public float moveForce = 25f;
    public float shrinkRate = 0.0007f;

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 input = Vector2.zero;
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else if (Application.platform == RuntimePlatform.Android) {
            input = Input.acceleration;
        }
        //Normalize input so that every direction has the same max force
        input = input.normalized;
        GetComponent<Rigidbody2D>().AddForce(input * moveForce);
        

        //If there was input given, spawn particles and shrink object
        //Length of the vector has to be longer than 0.2f because accelerometer would otherwise shrink the object even when "not moving"
        float inputLength = Mathf.Sqrt(Mathf.Pow(input.x, 2f) + Mathf.Pow(input.y, 2f));
        if (inputLength > 0.2f) {
            shrink();
            GetComponent<SpawnParticles>().spawnParticle();
        }
        //Prevents speed getting over the limit
        float velocityLength = Mathf.Sqrt(Mathf.Pow(GetComponent<Rigidbody2D>().velocity.x, 2f) + Mathf.Pow(GetComponent<Rigidbody2D>().velocity.y, 2f));
        if (velocityLength > speedLimit) {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speedLimit;
        }
    }

    private void shrink() {
        transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate);
        if (transform.localScale.x <= 0.10f || transform.localScale.y <= 0.10f || transform.localScale.z <= 0.10f) {
            GetComponent<GameEnd>().gameLost();
        }
    }
}