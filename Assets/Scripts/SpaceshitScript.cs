using UnityEngine;
using System.Collections;

public class SpaceshitScript : MonoBehaviour {

	public float angle = 90;
	public float anglespeed = 0;
	public float dist = 0.2f;
	public float hspeed = 0.05F;
	public float vspeed = 0.2f;
	public float haccel = 0.05F;
	public float deaccel = 0.01f;
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

		float hbound = Camera.main.orthographicSize * Camera.main.aspect;
		float vbound = Camera.main.orthographicSize * Camera.main.aspect;



		Vector3 position_2 = transform.position;

		if (Input.GetButton ("Fire1")&&allowfire) {
			allowfire = false;
			GameObject bullet = (GameObject)Instantiate (bullet1, new Vector3 (dist*Mathf.Cos(angle * Mathf.Deg2Rad), dist*Mathf.Sin(angle * Mathf.Deg2Rad), 1), Quaternion.identity);
			bullet.GetComponent<BulletScript>().angle = angle;
			bullet.GetComponent<BulletScript>().dist = dist;
			bullet.GetComponent<BulletScript>().maxdist = dist;
			bullet.GetComponent<BulletScript>().damage = 1;
			bullet.tag = "player_bullet";
			Invoke ("ReallowFire",0.1f);
		}

		if (Input.GetButton ("Fire2")&&allowfire) {
			PlayerPrefs.SetInt("highscore", GameScript.highscore); // The first is the string name that refers to the saved score, the second is your score variable (int)
			PlayerPrefs.Save();

			Application.LoadLevel("gameover");
		}

		if (Input.GetButton ("Fire3")&&allowfire) {
			GameScript.addScore(200);
		}


		anglespeed += movementX * haccel;
		if (anglespeed > hspeed) {
			anglespeed = hspeed;
		}
		if (anglespeed < -hspeed) {
			anglespeed = -hspeed;
		}
		anglespeed = anglespeed>0 ? anglespeed - deaccel : anglespeed + deaccel;
		angle += anglespeed;

		//float x = 
		//float y = 

		position_2.x = dist * Mathf.Cos(angle * Mathf.Deg2Rad);
		position_2.y = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
		position_2.z = -1;
		/*
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
		*/

		transform.rotation = Quaternion.Euler(new Vector3(45,0,angle+90));
		transform.position = position_2;
	}
}
