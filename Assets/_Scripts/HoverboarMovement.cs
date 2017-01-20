using UnityEngine;
using System.Collections;

public class HoverboarMovement : MonoBehaviour {


	public float maxSpeed = 250.0f;
	float currentSpeed = 0;

	[Range(0.01f, Mathf.PI)]
	public float scale = 1.5f;

	[Range(0.01f, 1.0f)]
	public float thresholdX = 0.01f;
	[Range(0.01f, 1.0f)]
	public float thresholdZ = 0.01f;


	public float speed = 10;
	public float rotationSpeed = 20;
	// Use this for initialization
	oscControl osc;
	Vector3 lastForceVec = Vector3.zero;
	ConstantForce force;
	string speedStr;
	Rigidbody rb;
	Vector3 lastPos;

	public int score;

	Quaternion calibration = Quaternion.identity;
	void OnGUI(){
		GUILayout.BeginHorizontal();
		GUIStyle labelStyle = new GUIStyle( GUI.skin.label);
		labelStyle.normal.textColor = Color.black;
		GUILayout.Label("speed: " + speedStr, labelStyle);
		if(GUILayout.Button("calibrate")){
			calibration = Quaternion.Euler(osc.rotation * scale);
		}
		GUILayout.Label("score: " + score, labelStyle);

		GUILayout.EndHorizontal();
	}


	void Start () {
		score = 0;
		osc = GetComponent<oscControl>();
//		force = GetComponent<ConstantForce>();
		lastPos = transform.position;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		float dist = Vector3.Distance(lastPos, transform.position);
		speedStr = (dist/Time.fixedDeltaTime).ToString();


		Vector3 flippedRot = new Vector3(-osc.rotation.z, 0, -osc.rotation.x);

		Debug.DrawLine(transform.position + Vector3.up * 2, Vector3.up * 2 + transform.position + flippedRot, Color.cyan);


		transform.rotation =  Quaternion.Euler(osc.rotation * scale);// Quaternion.RotateTowards( calibration, Quaternion.Euler(osc.rotation * scale), 90 );


		Vector3 forceVec = Vector3.zero;

		// speed
		if(Mathf.Abs(osc.rotation.x) > thresholdX){
			forceVec.x = -osc.rotation.z * rotationSpeed;
		}

		if(Mathf.Abs(osc.rotation.z) > thresholdZ){
			forceVec.z = osc.rotation.x * speed;
		}



		rb.velocity = forceVec;

		lastPos.Set(transform.position.x, transform.position.y, transform.position.z);
		lastForceVec.Set(forceVec.x, forceVec.y, forceVec.z);
	}


}
