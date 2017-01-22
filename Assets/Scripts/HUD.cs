using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text velocityLabel, scoreLabel, timeLabel;
	float velocity, score, time;
	void OnGUI(){

		Rect left = new Rect(0, 0, Screen.width/2, 50);
		Rect right = new Rect(Screen.width/2, 0, Screen.width/2, 50);

		GUILayout.BeginArea(left);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Speed: " +  ((int)(velocity * 10f)).ToString());
		GUILayout.Label(time + "s");
		GUILayout.Label("Score: " + score);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

		GUILayout.BeginArea(right);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Speed: " +  ((int)(velocity * 10f)).ToString());
		GUILayout.Label(time + "s");
		GUILayout.Label("Score: " + score);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
	}


	public void SetValues (int score, int time, float velocity) {
		this.score = score;
		this.time = time;
		this.velocity = velocity;

		velocityLabel.text = "Speed: " +  ((int)(velocity * 10f)).ToString();
		scoreLabel.text = "Points: " +  score.ToString();
		timeLabel.text = time.ToString() + "s";
	}
}