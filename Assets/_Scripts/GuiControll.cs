using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiControll : MonoBehaviour {

	public bool useKeyboard = false;
	public Text[] labels;
	public string[] sceneNames;
	public RectTransform selectionIndicator;

	private int selIndex, oldIndex;
	private float timeCounter;
	// Use this for initialization
	void Start () {
		timeCounter = 1;
		selIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime;
		if (timeCounter < 0.4) {
			return;
		}
		bool triggLeft = false;
		bool triggRight = false;
		bool triggConfirm = false;
		if (useKeyboard) {
			triggLeft = Input.GetAxis ("Horizontal") < -0.1;
			triggRight = Input.GetAxis ("Horizontal") > 0.1;
			triggConfirm = Input.GetAxis ("Submit") == 1;
		}
		if (triggLeft) {
			oldIndex = selIndex;
			selIndex = (((selIndex - 1) % labels.Length) + labels.Length) % labels.Length;
		}
		if (triggRight) {
			oldIndex = selIndex;
			selIndex = (((selIndex + 1) % labels.Length) + labels.Length) % labels.Length;
		}
		if (triggLeft || triggRight) {
			timeCounter = 0;
			labels [oldIndex].GetComponent<SineOscillator> ().neutral = true;
			labels [selIndex].GetComponent<SineOscillator> ().neutral = false;
			selectionIndicator.anchoredPosition = new Vector2(
				labels [selIndex].rectTransform.anchoredPosition.x,
				selectionIndicator.anchoredPosition.y
			);
		}
		if (triggConfirm) {
			timeCounter = 0;
			if (selIndex < sceneNames.Length) {
				AutoFade.LoadLevel (sceneNames [selIndex], 1, 1, Color.black);
			}
		}
	}
}
