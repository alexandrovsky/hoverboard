using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text distanceLabel, velocityLabel, scoreLabel;

	public void SetValues (float distanceTraveled, float velocity, int score) {
		distanceLabel.text = "Distance " + ((int)(distanceTraveled * 10f)).ToString();
		velocityLabel.text = "Velocity " + ((int)(velocity * 10f)).ToString();
		scoreLabel.text = "Score " + score.ToString();
	}
}