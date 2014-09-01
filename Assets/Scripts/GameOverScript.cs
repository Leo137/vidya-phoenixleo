using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public GameObject UIHighscore;

	// Use this for initialization
	void Start () {
		if (GameScript.highscore_beaten) {
			UIHighscore.guiTexture.enabled = true;
			StartCoroutine("ToHighScore");
		}
		else {
			UIHighscore.guiTexture.enabled = false;
			StartCoroutine("ToMainMenu");
		}
	}

	IEnumerator ToHighScore(){
		yield return new WaitForSeconds (3);
		Application.LoadLevel("highscore");
	}

	IEnumerator ToMainMenu(){
		yield return new WaitForSeconds (3);
		Application.LoadLevel("mainscreen");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
