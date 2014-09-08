using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	float speed = 0.035f;
	int type = 0;
	public float angle = 0;
	int health;
	public float dist = 0;
	public bool paused = false;
	public Sprite[] enemySprites;
	public int[] healths;
	int mode;
	int behaviour;
	
	public GameObject floatingScore;
	public GameObject shield;
	public GameObject LaserItem;

	
	// Use this for initialization
	void Start () {
		
		floatingScore.CreatePool(2);
		health = healths[type];
		GetComponent<SpriteRenderer>().sprite = enemySprites[type];
		mode = 0;
		behaviour = 1;
		
		transform.position = new Vector3 (0, 0);
		if(type == 1){
			GameObject g = (GameObject)Instantiate(shield);
			g.GetComponent<ShieldEnemyScript>().parent = this.gameObject;
			g.transform.parent = this.gameObject.transform;
			speed = 0.01f;
		}
	}
	
	public void setType(int x){
		type = x;
	}

	void OnEnable(){
		
		
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

		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+90)); 
		transform.position = position;
		transform.localScale = Vector3.one*(Mathf.Abs((dist+0.1f)/4)) * 1.5f;

		Vector3 hola = Vector3.one*(Mathf.Abs((dist+0.1f)/4)) * 1.5f;
		Collider2D collider2D = this.GetComponent<Collider2D> ();
		collider2D.bounds.size.Set (hola.x, hola.y, hola.z);

		//float hbound = Camera.main.orthographicSize * Camera.main.aspect;
		//float vbound = Camera.main.orthographicSize * Camera.main.aspect;
		
		/*if(transform.position.y <= -(vbound + 2)){
			Destroy (gameObject,0.0f);
		}
		else if(transform.position.y >= (vbound + 2)){
			Destroy (gameObject,0.0f);
		}*/
		if(type == 0){
			if (behaviour == 1) {
				if (dist >= 4.4f) {
						if (mode == 0) {
								mode++;
								Invoke ("changeMode", 5.0f);
						}
						if (mode == 1) {
								angle += speed * 18;
						}
						if (mode == 2) {
							mode++;
							Invoke ("changeMode", 2.0f);
						}
						if (mode == 3) {
							dist += speed;
						}
						if (mode == 4) {
								speed = -speed;
								mode = 3;
						}
						if (mode == 5) {
								dist += speed;
						}
				} else {
						angle += speed * 7;
						dist += speed * 0.3f;
				}
				if (dist < 0) {
						
						speed = -speed;
						mode = 0;
						angle = angle + 90 + Random.Range(40,90);
				}
			}
		}
		if(type == 1){
			if(dist < 5.5f){
				if(dist < 0){
					GetComponentInChildren<ShieldEnemyScript>().setBackwards(false);
					speed = -speed;
					mode = 0;
					angle = angle + 90 + Random.Range(40,90);
				}
				if(mode == 0){
					dist += speed;
				}
				if(mode == 1){
					dist += speed;
					angle += speed * 45;
				}
			}
			else{
				if(mode == 0){
					GetComponentInChildren<ShieldEnemyScript>().setBackwards(true);
					mode = 1;
					speed = -speed;
				}
				else{
					dist += speed;
				}
			}
		}
	}

	void changeMode(){
		mode ++;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.GetComponent<BulletScript>()!=null && collider.GetComponent<BulletScript>().tag == "player_bullet") {
			if(Mathf.DeltaAngle(angle,collider.GetComponent<BulletScript>().angle)>6.0f)
				return;
			if(Mathf.Abs(collider.GetComponent<BulletScript>().dist - dist)>1.10f)
				return;
			
			
			StartCoroutine("TintChange");
			collider.gameObject.Recycle();
			//coll.gameObject.Recycle();
			health -= collider.GetComponent<BulletScript>().damage;
			if(health<=0){
				if(SpaceshitScript.lasercd<=0&&Random.value>=(0.95+0.05*(GameScript.chance/10.0f))){
					GameObject g =(GameObject)Instantiate(LaserItem);
					g.GetComponent<ItemScript>().setPivotAngle(angle);
					GameScript.chance++;
				}
				if(type==0){
					GameScript.addScore(200);
					GameObject f = floatingScore.Spawn();
					f.transform.position = Camera.main.WorldToViewportPoint(transform.position);
					f.guiText.text = "200";
				}
				if(type==1){
					GameScript.addScore(500);
					GameObject f = floatingScore.Spawn();
					f.transform.position = Camera.main.WorldToViewportPoint(transform.position);
					f.guiText.text = "500";
				}
				gameObject.Recycle();
			}

			

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
