using UnityEngine;
using System.Collections;

public class EnemyScript1 : MonoBehaviour {

	float speed = 0.04f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3 (0, speed, 0);
		float hbound = Camera.main.orthographicSize * Camera.main.aspect;
		float vbound = Camera.main.orthographicSize * Camera.main.aspect;
		
		if(transform.position.y <= -(vbound + 2)){
			Destroy (gameObject,0.0f);
		}
		else if(transform.position.y >= (vbound + 2)){
			Destroy (gameObject,0.0f);
		}
	}
}
