using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour {

	public List<PowerUpType> powerUps = new List<PowerUpType>();

	public bool useKeyboard = true;
	[Range(0.01f, 1.0f)]
	public float thresholdX = 0.01f;
	[Range(0.01f, 1.0f)]
	public float thresholdZ = 0.01f;

	oscControl osc;
	public PipeSystem pipeSystem;

	public float startVelocity;
	public float rotationVelocity;

	public MainMenu mainMenu;
	public HUD hud;

	public float[] accelerations;

	private Pipe currentPipe;

	public float time = 99; // in seconds


	public float acceleration, velocity;
	private float distanceTraveled;
	private float deltaToRotation;
	private float systemRotation;
	private float worldRotation, avatarRotation;
	private int score;

	private Transform world, rotater;

	public void StartGame (int accelerationMode) {
		distanceTraveled = 0f;
		avatarRotation = 0f;
		systemRotation = 0f;
		worldRotation = 0f;
		acceleration = accelerations[accelerationMode];
		velocity = startVelocity;
		currentPipe = pipeSystem.SetupFirstPipe();
		SetupCurrentPipe();
		gameObject.SetActive(true);
		hud.SetValues(distanceTraveled, velocity, score, (int)time);
	}

	public void AddScore(int value=1){
		score += value;
	}

	public void Die () {
		osc.Vibrate();
		//mainMenu.EndGame(distanceTraveled);
		//gameObject.SetActive(false);
	}

	private void Start () {
		osc = GetComponent<oscControl>();
		world = pipeSystem.transform.parent;
		rotater = transform.GetChild(0);
		gameObject.SetActive(false);
	}

	private void Update () {

		time = time-Time.deltaTime;
		if(time <= 0){
			Die();
		}

		velocity += acceleration * Time.deltaTime;
		float delta = velocity * Time.deltaTime;
		distanceTraveled += delta;
		systemRotation += delta * deltaToRotation;

		if (systemRotation >= currentPipe.CurveAngle) {
			delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
			currentPipe = pipeSystem.SetupNextPipe();
			SetupCurrentPipe();
			systemRotation = delta * deltaToRotation;
		}

		pipeSystem.transform.localRotation =
			Quaternion.Euler(0f, 0f, systemRotation);

		UpdateAvatarRotation();

		hud.SetValues(distanceTraveled, velocity, score, (int)time);
	}

	private void UpdateAvatarRotation () {
		float rotationInput = 0f;
		if (Application.isMobilePlatform) {
			if (Input.touchCount == 1) {
				if (Input.GetTouch(0).position.x < Screen.width * 0.5f) {
					rotationInput = -1f;
				}
				else {
					rotationInput = 1f;
				}
			}
		}
		else {
			if(useKeyboard){
				rotationInput = Input.GetAxis("Horizontal"); //
			}else{
				rotationInput = osc.rotation.x * 10;
			}

		}
		avatarRotation += rotationVelocity * Time.deltaTime * rotationInput;
		if (avatarRotation < 0f) {
			avatarRotation += 360f;
		}
		else if (avatarRotation >= 360f) {
			avatarRotation -= 360f;
		}
		rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
	}

	private void SetupCurrentPipe () {
		deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
		worldRotation += currentPipe.RelativeRotation;
		if (worldRotation < 0f) {
			worldRotation += 360f;
		}
		else if (worldRotation >= 360f) {
			worldRotation -= 360f;
		}
		world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
	}
}