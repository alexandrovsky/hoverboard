using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineOscillator : MonoBehaviour {

	public float amplitudeX = 10;
	public float amplitudeY = 0;
	public float speed = 1f;
	public bool neutral = true;

	private RectTransform rect;
	private Vector2 startPos;
	private Vector2 endPos;
	private float timeCounter;

	// Use this for initialization
	void Start () {
		rect = gameObject.GetComponent<RectTransform> ();
		startPos = rect.anchoredPosition;
		timeCounter = 0;
	}

	// Update is called once per frame
	void Update () {
		if (neutral) {
			rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, startPos, Time.deltaTime);
			timeCounter = 0;
		} else {
			rect.anchoredPosition = new Vector2 (
				startPos.x + Mathf.Sin (timeCounter * speed) * amplitudeX, 
				startPos.y + Mathf.Cos (timeCounter * speed + Mathf.PI/2) * amplitudeY
			);
			timeCounter += Time.deltaTime;
		}
	}
}
