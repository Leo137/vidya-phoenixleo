using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	public GUIText highscore;

	// Use this for initialization
	void Start () {

		GameScript.highscore = PlayerPrefs.GetInt("highscore");

		highscore.text = "Highscore: "+PlayerPrefs.GetString("name")+"  "+GameScript.highscore.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
