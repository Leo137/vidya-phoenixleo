using UnityEngine;
using System.Collections;

public class ShieldEnemyScript : MonoBehaviour {

	float angle = 0.0f;
	float dist = 0.4f;
	bool backwards = false;
	public GameObject parent;
	// Use this for initialization
	void Start () {
		angle = parent.GetComponent<EnemyScript>().angle;
	}
	
	void setAngle(int x){
		angle = x;
	}
	
	public void setBackwards(bool t){
		backwards = t;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		
		
		position.x = parent.transform.position.x + dist * Mathf.Cos(angle * Mathf.Deg2Rad);
		position.y = parent.transform.position.y + dist * Mathf.Sin(angle * Mathf.Deg2Rad);
		angle = backwards ? parent.GetComponent<EnemyScript>().angle + 180 :parent.GetComponent<EnemyScript>().angle;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+90)); 
		transform.position = position;
		//transform.localScale = parent.transform.localScale * 0.5f;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.GetComponent<BulletScript>()!=null && collider.GetComponent<BulletScript>().tag == "player_bullet") {
			
			StartCoroutine("TintChange");
			collider.gameObject.Recycle();
			
			
		}
		//coll.gameObject.tag = "Finish";
		//Instantiate (ball, new Vector3 (x, y, 0), Quaternion.identity);
	}
	
	IEnumerator TintChange(){
		int x = 0;
		for (x=0; x<4; x++) {
			this.gameObject.renderer.material.color = Color.yellow;
			yield return new WaitForSeconds (0.02f);
			this.gameObject.renderer.material.color = Color.black;
			yield return new WaitForSeconds (0.02f);
			this.gameObject.renderer.material.color = Color.white;
			yield return new WaitForSeconds (0.02f);
		}
	}
}
