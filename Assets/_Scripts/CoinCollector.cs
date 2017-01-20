using UnityEngine;
using System.Collections;

public class CoinCollector : MonoBehaviour {

	// Use this for initialization
	HoverboarMovement hoverboad;
	void Start () {
		hoverboad = GameObject.FindGameObjectWithTag("Player").GetComponent<HoverboarMovement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.CompareTag("Collectable")){
			hoverboad.score++;
			GameObject.Destroy(collision.gameObject);
		}

	}
}
