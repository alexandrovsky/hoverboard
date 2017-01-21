using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public enum PowerUpType{
		Time,
		Speed,
		Magnet
	};

	public int cooldown; // in seconds
	public float value;
	public PowerUpType type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
