using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;
	public float angle;
	public float dist;
	public float maxdist;
	public int damage;
	// Use this for initialization
	void Start () {

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

		if(dist <=0){
			Destroy (gameObject,0.0f);
		}
	}
}
