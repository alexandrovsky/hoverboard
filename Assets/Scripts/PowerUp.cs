using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType{
	Time,
	Speed,
	Magnet
};

public class PowerUp : MonoBehaviour {



	public int cooldown = 10; // in seconds
	public float value;
	public PowerUpType type;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public virtual IEnumerator Callback(Player player){
		yield return null;
	}
}
