using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	float speed = 0.013f;
	public int type = 0;
	public float angle = 0;
	public int health = 3;
	float dist = 0;
	public bool paused = false;
	// Use this for initialization
	void Start () {
	
	}

	void Unpause(){
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (paused) {
			transform.localScale = Vector3.one * 0;
			return;
		}

		Vector3 position = transform.position;
		position.x = dist * Mathf.Cos(angle * Mathf.Deg2Rad);
		position.y = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
		dist += speed;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+90)); 
		transform.position = position;
		transform.localScale = Vector3.one*(Mathf.Abs((dist+0.1f)/4)) * 1.5f;

		float hbound = Camera.main.orthographicSize * Camera.main.aspect;
		float vbound = Camera.main.orthographicSize * Camera.main.aspect;
		
		if(transform.position.y <= -(vbound + 2)){
			Destroy (gameObject,0.0f);
		}
		else if(transform.position.y >= (vbound + 2)){
			Destroy (gameObject,0.0f);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		Collider2D collider = coll.collider;
		if (collider.GetComponent<BulletScript>().tag == "player_bullet") {
			health -= collider.GetComponent<BulletScript>().damage;
			if(health<=0){
				GameScript.addScore(200);
				Destroy (gameObject,0.0f);
			}

			Destroy(collider.gameObject,0.0f);

			StartCoroutine("TintChange");



		}
		//coll.gameObject.tag = "Finish";
		//Instantiate (ball, new Vector3 (x, y, 0), Quaternion.identity);
	}

	IEnumerator TintChange(){
		int x = 0;
		for (x=0; x<4; x++) {
						gameObject.renderer.material.color = Color.yellow;
						yield return new WaitForSeconds (0.02f);
						gameObject.renderer.material.color = Color.black;
						yield return new WaitForSeconds (0.02f);
						gameObject.renderer.material.color = Color.white;
						yield return new WaitForSeconds (0.02f);
				}
	}
}
