using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text velocityLabel, scoreLabel, timeLabel;

	public void SetValues (int score, int time, float velocity) {
		
		velocityLabel.text = "Speed: " +  ((int)(velocity * 10f)).ToString();
		scoreLabel.text = "Points: " +  score.ToString();
		timeLabel.text = time.ToString() + "s";
	}
}