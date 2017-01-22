using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBoost : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override IEnumerator Callback(Player player){
		player.powerUps.Add(PowerUpType.Speed);
		float old = player.velocity;
		player.velocity += value;
		yield return new WaitForSeconds(cooldown);
		int i = -1;
		for(i = 0; i < player.powerUps.Count; i++){
			if(player.powerUps[i] == PowerUpType.Speed){
				break;
			}
		}
		if(i >= 0){
			player.powerUps.RemoveAt(i);
		}
		player.velocity = old;
		Debug.Log("power up deactivate");
	}
}
