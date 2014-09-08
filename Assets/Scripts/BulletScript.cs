using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;
	public float angle;
	public float dist;
	public float maxdist;
	public int damage;
	public TrailRenderer trail;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}

	void onEnable (){
		//Invoke ("ResetTrails", 0.01f);
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 position = transform.position;

		position.x = dist * Mathf.Cos(angle * Mathf.Deg2Rad);
		position.y = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
		dist -= speed;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+90)); 
		transform.position = position;
		transform.localScale = Vector3.one*(Mathf.Abs((dist+0.1f)/maxdist)) * 2;
		
		Vector3 hola = Vector3.one*(Mathf.Abs((dist+0.1f)/4)) * 0.2f;
		Collider2D collider2D = this.GetComponent<Collider2D> ();
		collider2D.bounds.size.Set (hola.x, hola.y, hola.z);

		if(dist <=0){
			gameObject.Recycle();
		}
		renderer.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D coll){
		Collider2D collider = coll.collider;
		if (collider.GetComponent<SpaceshitScript> () == null) {
						//gameObject.Recycle ();
				}
	}
}
