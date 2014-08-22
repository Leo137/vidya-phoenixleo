using UnityEngine;
using System.Collections;

public class Background2Script : MonoBehaviour {

	//the point the background goes back to start
	private float reset = -5.1f;
	
	//the point where the background goes after a reset
	private float startPoint = 5f;
	private float startPointx;
	private float startPointz;
	//the speed of movement
	public float speed;
	
	public GameObject otherBackground;
	
	
	void Update () {

		
		//if the background is at reset point, move it to startPoint
		if(transform.position.y <= reset) {
			transform.position = (new Vector3(startPointx, startPoint, startPointz));
		}
		
		//EnemySpawner.speedPlus is a static float
		
		//Move the backgound up
		transform.position -= new Vector3(0,speed,0);
	}

	void Start(){
		startPointx = transform.position.x;
		startPointz = transform.position.z;
	}
}
