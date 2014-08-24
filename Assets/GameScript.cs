using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject enemy1;

	public GameObject wave_label;
	public GameObject score_label;
	public GameObject life_label;

	private int enemys = 1;
	private int waves = 4;
	private int wave = 0;
	private int[][] enemy_number;

	// Use this for initialization
	void Start () {
		int x;
		enemy_number = new int[enemys][];
		for (x=0; x<enemys; x++) {
			enemy_number[x] = new int[waves];
		}

		//enemy 1...
		enemy_number [0] [0] = 3;
		enemy_number [0] [1] = 3;
		enemy_number [0] [2] = 5;
		enemy_number [0] [3] = 6;

		Invoke ("SpawnWave", 5f);
	}

	void SpawnWave(){
		int x = 0;
		int y = 0;

		int positionx = 0;
		int positiony = 0;
		float offsetx = 0.55f;
		float offsety = 0.55f;
		int rowsize = 4;
		float originalposx = -2.0f;
		float originalposy = 3.0f;

		for (x=0; x<enemys; x++) {
			for(y=0;y<enemy_number[x][wave];y++){
				Instantiate (enemy1, 
				new Vector3 (originalposx+positionx*offsetx,
				             originalposy+positiony*offsety,
				             0),
				Quaternion.identity);
				if(positionx > rowsize){
					positionx = 0;
					positiony ++;
				}
				else{
					positionx++;
				}
			}
		}

		wave++;
		Invoke ("SpawnWave", 50f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
