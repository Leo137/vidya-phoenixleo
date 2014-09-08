using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject enemy1;

	public GameObject wave_label;
	public GameObject score_label;
	public GameObject highscore_label;
	public GameObject FloatingScore;

	private int enemys = 2;
	private int waves = 4;
	private int wave = 0;
	int oldWave = 0;
	private int[][] enemy_number;
	public static int chance = 0;

	public static int score = 0;
	int oldScore = 0;
	public static int highscore;

	public static int lives = 3;

	public static bool highscore_beaten = false;

	bool spawned = false;

	// Use this for initialization
	void Start () {
		enemy1.CreatePool (10000);
		lives = 3;
		score = 0;
		highscore_beaten = false;
		int x;
		enemy_number = new int[enemys][];
		for (x=0; x<enemys; x++) {
			enemy_number[x] = new int[waves];
		}

		//enemy 1...
		enemy_number [0] [0] = 50;
		enemy_number [0] [1] = 55;
		enemy_number [0] [2] = 60;
		enemy_number [0] [3] = 12;
		
		//enemy 1...
		enemy_number [1] [0] = 0;
		enemy_number [1] [1] = 5;
		enemy_number [1] [2] = 10;
		enemy_number [1] [3] = 4;

		Invoke ("SpawnWave", 5f);
	}

	void SpawnWave(){
		//System.GC.Collect();
		int x = 0;
		int y = 0;

		int angle = 0;
		int rowsize = 6;
		int row = 0;
		float originalposx = -2.0f;
		float originalposy = 3.0f;



		for (x=0; x<enemys; x++) {
			angle = 0;
			for(y=0;y<enemy_number[x][wave];y++){
				GameObject e = (GameObject)enemy1.Spawn( 
				new Vector3 (originalposx,
				             originalposy,
				             0),
				Quaternion.identity);
				e.GetComponent<EnemyScript>().setType(x);
				e.GetComponent<EnemyScript>().angle = angle;
				e.GetComponent<EnemyScript>().paused = true;
				e.GetComponent<EnemyScript>().Invoke("Unpause",2.0f*row);
				angle += 360/rowsize;
				if(angle>=360){

					row++;
					angle = 0 +  (row%5) * 55;
				}
			}
		}

		spawned = true;

		wave++;
		GameObject f = FloatingScore.Spawn();
		f.transform.position = new Vector3(0.5f,0.5f,0.5f);
		f.guiText.text = "Ronda "+wave.ToString();
		f.guiText.fontSize = 30;
		//Invoke ("SpawnWave", 50f);
	}
	
	// Update is called once per frame
	void Update () {

		if (score != oldScore) {
			highscore_label.guiText.text = highscore.ToString ();
			score_label.guiText.text = score.ToString ();
			oldScore = score;
		}

		if (wave != oldWave) {
			wave_label.guiText.text = wave.ToString ();
			oldWave = wave;
		}
		if (Time.frameCount % 300 == 0) {
			int enemy = 0;
			foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject))) {
					if (go.GetComponent<EnemyScript> () != null)
							enemy++;
			}
			if (enemy == 0 && spawned) {
					CancelInvoke ("SpawnWave");
					spawned = false;
					Invoke ("SpawnWave", 0.0f);
			}
		}
	}

	public static void addScore(int points){
		score += points;
		if (score > highscore) {
			highscore_beaten = true;
			highscore = score;
		}
	}
	
	float position_x = 20.0f;
	float position_y = 32.0f;
	float spacing_x = 18.0f;
	Vector2 size;
	Vector2 pos;
	public Texture2D liveIcon;

	void OnGUI(){
		int x;
		if (pos == Vector2.zero) {
			pos = new Vector2(position_x,position_y);
		}
		for(x=0;x<lives;x++){
			GUI.DrawTexture(new Rect( pos.x+spacing_x*x, pos.y,16,16), liveIcon);
		}
	}
}
