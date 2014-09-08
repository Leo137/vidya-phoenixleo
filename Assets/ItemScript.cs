using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
	
	float pivotangle;
	float angle = 0.0f;
	float dist = 0.0f;
	float amplitude = 20.0f;
	public int type;
	// Use this for initialization
	
	public void setPivotAngle(float f){
		pivotangle = f;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		
		
		position.x =  dist * Mathf.Cos(angle * Mathf.Deg2Rad);
		position.y =  dist * Mathf.Sin(angle * Mathf.Deg2Rad);
		
		angle = pivotangle + amplitude * Mathf.Sin(Time.time*3.0f);
		
		dist += 0.02f;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+90)); 
		transform.position = position;
		transform.localScale = Vector3.one*(Mathf.Abs((dist+0.1f)/4)) * 1.5f;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.GetComponent<BulletScript>()!=null && collider.GetComponent<BulletScript>().tag == "player_bullet") {
			
			
			
		}
		//coll.gameObject.tag = "Finish";
		//Instantiate (ball, new Vector3 (x, y, 0), Quaternion.identity);
	}
}
