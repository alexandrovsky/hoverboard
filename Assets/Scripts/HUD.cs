using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text distanceLabel, velocityLabel, scoreLabel, timeLabel;

	public void SetValues (float distanceTraveled, float velocity, int score, int time) {
		distanceLabel.text = ((int)(distanceTraveled * 10f)).ToString();
		velocityLabel.text = ((int)(velocity * 10f)).ToString();
		scoreLabel.text = score.ToString();
		timeLabel.text = time.ToString();
	}
}