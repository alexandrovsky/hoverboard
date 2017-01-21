using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreStore : MonoBehaviour {

	public Text firstColumn;
	public Text secondColumn;
	public Text thirdColumn;

	private int hsMax = 30;
	private List<Score> hsList;
	private string dotTemplate = "• • • • • • • • • • • • • • • • • • • • • • • ";
	private List<Text> lines;

	// Use this for initialization
	void Start () {
		InitHighscores ();
		UpdateVisuals ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitHighscores () {
		hsList = new List<Score>();
		lines = new List<Text> ();
		string name;
		int value;
		Score score;
		firstColumn.enabled = false;
		secondColumn.enabled = false;
		thirdColumn.enabled = false;
		for (int i = 0; i < hsMax; i++) {
			name = PlayerPrefs.GetString ("HighscoreName-"+i, "NOBODY");
			value = PlayerPrefs.GetInt ("HighscoreVal-" + i, 0);
			score = new Score ();
			score.name = name;
			score.value = value;
			hsList.Add (score);
		}

	}

	void UpdateVisuals () {
		clearLines ();
		Text hsLine;
		Text curColumn = firstColumn;
		int verOffset = 0;
		string nStr, dotStr, vStr;
		for (int i=0; i < hsList.Count; i++) {
			if (i == 10) {
				curColumn = secondColumn;
				verOffset = 0;
			} else if (i == 20) {
				curColumn = thirdColumn;
				verOffset = 0;
			}
			hsLine = Instantiate(firstColumn, new Vector3(0, 0, 0), Quaternion.identity) as Text;
			lines.Add (hsLine);
			hsLine.transform.SetParent(gameObject.transform, false);
			hsLine.transform.position = new Vector3(curColumn.transform.position.x,
				curColumn.transform.position.y - verOffset,
				curColumn.transform.position.z);
			nStr = string.Format("{0}. {1}", i + 1, hsList[i].name);
			vStr = hsList [i].value.ToString ();
			int dotCount = (30 - (nStr.Length + vStr.Length));
			dotStr = dotTemplate.Substring(0, dotCount);

			hsLine.text = string.Format("{0} {1} {2}", nStr, dotStr, vStr);
			hsLine.enabled = true;
			verOffset += 30;
		}
	}

	void AddScore(string name, int value) {
		Score sc = new Score ();
		sc.value = value;
		sc.name = name;
		hsList.Add (sc);
		hsList.Sort(delegate(Score c1, Score c2) { return c2.value.CompareTo(c1.value); });
		if(hsList.Count > 30){
			hsList.RemoveRange (30, hsList.Count - 30);
		}
		for (int i = 0; i < hsList.Count; i++) {
			PlayerPrefs.SetString ("HighscoreName-" + i, hsList [i].name);
			PlayerPrefs.SetInt ("HighscoreVal-" + i, hsList [i].value);
		}
		UpdateVisuals ();
	}

	void clearLines(){
		foreach (Text t in lines){
			Destroy (t);
		}
		lines.Clear ();
	}
}

public class Score
{
	public int value;
	public string name;

}