using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrldPlayer : MonoBehaviour {
	oscControl osc;
	public float velocity, rotationVelocity;
	private int score;
	public bool keyboardControl = true;
	Rigidbody rb;
	// Use this for initialization
	private void Start () {
		osc = GetComponent<oscControl>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 vel = Vector3.zero;
		Quaternion rot = rb.rotation;
		if(keyboardControl){
			rot = rb.rotation * Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationVelocity * Time.fixedDeltaTime, 0);
			vel = transform.forward * Input.GetAxis("Vertical") * velocity * Time.fixedDeltaTime;
		}else {
			rot = rb.rotation * Quaternion.Euler(0, osc.rotation.x * rotationVelocity * Time.fixedDeltaTime, 0);
			vel = transform.forward * -osc.rotation.z * velocity * Time.fixedDeltaTime;
		}
		rb.rotation = rot;
		rb.velocity = vel;
	}
}
