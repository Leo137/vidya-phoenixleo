using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, speed, 0);
		float hbound = Camera.current.orthographicSize * Camera.current.aspect;
		float vbound = Camera.current.orthographicSize * Camera.current.aspect;

		if(transform.position.y <= -(vbound + 2)){
			Destroy (gameObject,0.0f);
		}
		if(transform.position.y >= (vbound + 2)){
			Destroy (gameObject,0.0f);
		}
	}
}
