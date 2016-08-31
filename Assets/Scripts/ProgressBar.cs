using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    private GameObject player;
    private float originalAnchoredX;

	// Use this for initialization
	void Start () {
        originalAnchoredX = GetComponent<RectTransform>().anchoredPosition.x - GetComponent<RectTransform>().sizeDelta.x / 2;
        player = GameObject.FindGameObjectWithTag("Player");
        float size = player.transform.localScale.x / player.GetComponent<GameEnd>().winScale * 100f;
        GetComponent<RectTransform>().sizeDelta = new Vector2(size, 100f);
        float newX = player.transform.localScale.x / player.GetComponent<GameEnd>().winScale * 100f;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(newX / 2 + originalAnchoredX, GetComponent<RectTransform>().anchoredPosition.y);
    }
	
	// Update is called once per frame
	void Update () {
        float newX = player.transform.localScale.x / player.GetComponent<GameEnd>().winScale * 100f;
        if (newX > 100f) newX = 100f;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(newX / 2 + originalAnchoredX, GetComponent<RectTransform>().anchoredPosition.y);
        GetComponent<RectTransform>().sizeDelta = new Vector2(newX, 100f);
        
    }
}
