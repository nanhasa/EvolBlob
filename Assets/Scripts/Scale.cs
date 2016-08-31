using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {

    public float growthRate = 0.1f;
    public float maxScale = 5f;

    public void grow()
    {
        if (transform.localScale.x < maxScale)
            transform.localScale += new Vector3(growthRate, growthRate, growthRate);
        preventSearchRadiusGrowth();
    }

    public void grow(float eatenScale)
    {
        if (transform.localScale.x < maxScale - 1f)
        {
            float rate = eatenScale / 3;
            transform.localScale += new Vector3(rate, rate, rate);
        }
        preventSearchRadiusGrowth();
    }

    public void preventSearchRadiusGrowth()
    {
        Component[] circleCol = GetComponentsInChildren<CircleCollider2D>();
        foreach (CircleCollider2D child in circleCol)
        {
            if (child.tag == "SearchRadius")
            {
                float childScale = 1 / transform.localScale.x + (1 / (2 * transform.localScale.x)); //Bigger blobs see a little further
                child.transform.localScale = new Vector3(childScale, childScale, childScale);
            }
        }
    }

    public void randomizeScale()
    {
        //randomize new scale and use the old scale as a base to keep the game hard enough
        float random = Random.Range(0.75f, 1.25f);
        float newScale = transform.localScale.x * random;
        if (newScale > 5f) newScale = 5f;
        transform.localScale = new Vector3(newScale, newScale, newScale);

        //Prevent child object from scaling
        preventSearchRadiusGrowth();
    }
}
