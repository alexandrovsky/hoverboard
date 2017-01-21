using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineOscillator : MonoBehaviour {

	public float amplitudeX = 10;
	public float amplitudeY = 0;
	public float speed = 1f;

	private RectTransform rect;
	private Vector2 startPos;
	private Vector2 endPos;

	// Use this for initialization
	void Start () {
		rect = gameObject.GetComponent<RectTransform> ();
		startPos = rect.anchoredPosition;
	}

	// Update is called once per frame
	void Update () {
		rect.anchoredPosition = new Vector2(
			startPos.x + Mathf.Sin(Time.time * speed) * amplitudeX, 
			startPos.y + Mathf.Sin(Time.time * speed) * amplitudeY
		);
	}
}
