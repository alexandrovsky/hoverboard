using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text distanceLabel, velocityLabel, scoreLabel;

	public void SetValues (float distanceTraveled, float velocity, int score) {
		distanceLabel.text = ((int)(distanceTraveled * 10f)).ToString();
		velocityLabel.text = ((int)(velocity * 10f)).ToString();
		scoreLabel.text = score.ToString();
	}
}