using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject enemy1;

	public GameObject wave_label;
	public GameObject score_label;
	public GameObject highscore_label;
	public GameObject life_label;

	private int enemys = 1;
	private int waves = 4;
	private int wave = 0;
	private int[][] enemy_number;

	public static int score = 0;
	public static int highscore;

	public static int lives = 3;

	public static bool highscore_beaten = false;


	// Use this for initialization
	void Start () {
		lives = 3;
		score = 0;
		highscore_beaten = false;
		int x;
		enemy_number = new int[enemys][];
		for (x=0; x<enemys; x++) {
			enemy_number[x] = new int[waves];
		}

		//enemy 1...
		enemy_number [0] [0] = 1;
		enemy_number [0] [1] = 3;
		enemy_number [0] [2] = 5;
		enemy_number [0] [3] = 6;

		Invoke ("SpawnWave", 5f);
	}

	void SpawnWave(){
		int x = 0;
		int y = 0;

		int angle = 0;
		int rowsize = 10;
		int row = 0;
		float originalposx = -2.0f;
		float originalposy = 3.0f;

		for (x=0; x<enemys; x++) {
			for(y=0;y<enemy_number[x][wave];y++){
				GameObject e = (GameObject)Instantiate (enemy1, 
				new Vector3 (originalposx,
				             originalposy,
				             0),
				Quaternion.identity);
				e.GetComponent<EnemyScript>().type = x;
				e.GetComponent<EnemyScript>().angle = angle;
				e.GetComponent<EnemyScript>().paused = true;
				e.GetComponent<EnemyScript>().Invoke("Unpause",2.0f*row);
				angle += 360/rowsize;
				if(angle>=360){
					angle = 0;
					row++;
				}
			}
		}

		wave++;
		Invoke ("SpawnWave", 50f);
	}
	
	// Update is called once per frame
	void Update () {

		highscore_label.guiText.text = highscore.ToString ();
		score_label.guiText.text = score.ToString ();

		life_label.guiText.text = lives.ToString ();
		wave_label.guiText.text = wave.ToString();


		
	}

	public static void addScore(int points){
		score += points;
		if (score > highscore) {
			highscore_beaten = true;
			highscore = score;
		}
	}
}
