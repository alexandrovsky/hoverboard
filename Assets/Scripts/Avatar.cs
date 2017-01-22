﻿using UnityEngine;
using System.Linq;
public class Avatar : MonoBehaviour {

	public ParticleSystem shape, trail, burst;

	private Player player;

	public float deathCountdown = -1f;

	private void Awake () {
		player = transform.root.GetComponent<Player>();
	}

	private void OnTriggerEnter (Collider collider) {

		if(collider.CompareTag("Collectable")){
			player.AddScore();
			Destroy(collider.gameObject);
		}else if(collider.CompareTag("PowerUp")){
			PowerUp pu = collider.gameObject.GetComponent<PowerUp>();
			switch(pu.type){
			case PowerUpType.Magnet:
				break;
			case PowerUpType.Speed:
				bool found = false;
				foreach(PowerUpType type in player.powerUps){
					if(type == PowerUpType.Speed){
						found = true;
						break;
					}
				}
				if(!found){
					StartCoroutine(pu.Callback(player));
				}else{
					
				}

				break;
			case PowerUpType.Time:
				player.time += pu.value;
				break;
			
			}

		}else if(collider.CompareTag("Obsticle")){
			if (deathCountdown < 0f) {
				shape.enableEmission = false;
				trail.enableEmission = false;
				burst.Emit(burst.maxParticles);
				deathCountdown = burst.startLifetime;
			}
		}
	}
	
	private void Update () {
		if (deathCountdown >= 0f) {
			deathCountdown -= Time.deltaTime;
			if (deathCountdown <= 0f) {
				deathCountdown = -1f;
				shape.enableEmission = true;
				trail.enableEmission = true;
				player.Die();
			}
		}
	}
}