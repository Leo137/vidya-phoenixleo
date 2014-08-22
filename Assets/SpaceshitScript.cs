using UnityEngine;
using System.Collections;

public class SpaceshitScript : MonoBehaviour {

	public float hspeed = 0.05F;
	public float vspeed = 0.2f;
	public GameObject bullet1;
	private bool allowfire;

	// Use this for initialization
	void Start () {
		allowfire = true;
	}

	void ReallowFire(){
		allowfire = true;
	}
	// Update is called once per frame
	void Update () {
		float movementX = Input.GetAxis ("Horizontal") ;
		float movementY = Input.GetAxis ("Vertical");
		float hbound = Camera.current.orthographicSize * Camera.current.aspect;
		float vbound = Camera.current.orthographicSize * Camera.current.aspect;



		Vector3 position_2 = transform.position;
		position_2.x = position_2.x + (movementX) * hspeed;
		position_2.y = position_2.y + (movementY) * vspeed;
		if (Input.GetButton ("Fire1")&&allowfire) {
			allowfire = false;
			Instantiate (bullet1, transform.position + new Vector3 (0, 0.1f, 1), Quaternion.identity);
			Invoke ("ReallowFire",0.1f);
		}

		if (position_2.x >=  hbound ) {
			position_2.x =  hbound ;
		}
		if (position_2.x <= - hbound ) {
			position_2.x = - hbound ;
		}
		if (position_2.y >=  vbound ) {
			position_2.y =  vbound ;
		}
		if (position_2.y <= - vbound ) {
			position_2.y = - vbound ;
		}

		transform.position = position_2;
	}
}
