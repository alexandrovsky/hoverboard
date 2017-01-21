using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreLogic : MonoBehaviour {

	public string backToScene;
	public bool useKeyboard;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool triggConfirm = false;
		if (useKeyboard) {
			triggConfirm = Input.GetAxis ("Submit") == 1;
		}
		if (triggConfirm) {
			AutoFade.LoadLevel (backToScene, 1, 1, Color.black);
		}
	}
}
