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
	public GameObject shield;
	private bool allowfire;
	public float hitpoints = 100.0f;
	public float shieldcd = 0.0f;
	public static float lasercd = 0.0f;
	bool returning = false;
	public Sprite bullet2;

	// Use this for initialization
	void Start () {

		bullet1.CreatePool (30000);
		
		allowfire = true;
	}

	void ReallowFire(){
		allowfire = true;
	}
	// Update is called once per frame
	void Update () {
		shieldcd -= 0.09f;
		lasercd -= 0.11f;
		if(returning){
			if(dist > 5.4f){
				dist -= 0.1f;
				StartCoroutine("TintChange");
			}
			else{
				dist = 5.4f;
				returning = false;
			}
		}
		
		float movementX = Input.GetAxis ("Horizontal") ;
		float movementY = Input.GetAxis ("Vertical");

		float hbound = Camera.main.orthographicSize * Camera.main.aspect;
		float vbound = Camera.main.orthographicSize * Camera.main.aspect;



		Vector3 position_2 = transform.position;

		if( !returning && Input.GetButton ("Fire1")&&allowfire) {
			allowfire = false;
			//GameObject bullet = (GameObject)Instantiate(bullet1, new Vector3 (dist*Mathf.Cos(angle * Mathf.Deg2Rad), dist*Mathf.Sin(angle * Mathf.Deg2Rad), 1), Quaternion.identity);
			GameObject bullet = bullet1.Spawn(new Vector3 ((dist-0.8f)*Mathf.Cos(angle * Mathf.Deg2Rad), (dist-0.8f)*Mathf.Sin(angle * Mathf.Deg2Rad), 1), Quaternion.identity);
			bullet.GetComponent<BulletScript>().angle = angle;
			bullet.GetComponent<BulletScript>().dist = dist-0.8f;
			bullet.GetComponent<BulletScript>().maxdist = dist-0.8f;
			bullet.GetComponent<BulletScript>().damage = 1;
			bullet.tag = "player_bullet";
			if(lasercd<=0)Invoke ("ReallowFire",0.1f);
			else {
				Invoke ("ReallowFire",0.02f);
				bullet.GetComponent<SpriteRenderer>().sprite = bullet2;
				bullet.GetComponent<BulletScript>().damage = 2;
				bullet.GetComponent<BulletScript>().speed = 0.19f;
				
				Color nColor = new Color(Random.value, Random.value, Random.value, Random.value);
				Material m = bullet.GetComponent<TrailRenderer>().material;
				m.SetColor ("_Color",nColor);
			}
		}

		if (!returning && Input.GetButtonDown ("Fire2")&&allowfire && shieldcd <= 0.0f) {
			shieldcd = 100.0f;
			GameObject s = (GameObject)Instantiate(shield,transform.position, Quaternion.identity );
			s.GetComponent<ShieldScript>().spaceshit = gameObject;
			

			/*
			PlayerPrefs.SetInt("highscore", GameScript.highscore); // The first is the string name that refers to the saved score, the second is your score variable (int)
			PlayerPrefs.Save();

			Application.LoadLevel("gameover");*/
		}

		if (Input.GetButton ("Fire3")&&allowfire) {
			//GameScript.addScore(200);
		}

		if(!returning){
		anglespeed += movementX * haccel;
		if (anglespeed > hspeed) {
			anglespeed = hspeed;
		}
		if (anglespeed < -hspeed) {
			anglespeed = -hspeed;
		}
		anglespeed = anglespeed>0 ? anglespeed - deaccel : anglespeed + deaccel;
		angle += anglespeed;
		}

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

		Vector3 hola = transform.localScale;
		Collider2D collider2D = this.GetComponent<Collider2D> ();
		collider2D.bounds.size.Set (hola.x, hola.y, hola.z);
	}

	void OnTriggerEnter2D(Collider2D collider){
		//Collider2D collider = coll.collider;
		if( collider.GetComponent<ItemScript>()!=null){
			
			switch(collider.GetComponent<ItemScript>().type){
				case 0:
					lasercd = 100;
					break;
				default:
					break;
			}
			Destroy(collider.gameObject);
		}
		if (collider.tag != "player_bullet" && collider.GetComponent<EnemyScript>() != null) {
			/*if(Mathf.Abs(collider.GetComponent<EnemyScript>().dist - dist)>1.5f)
				return;*/

			int shield_units = 0;
			foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject))){
				if(go.GetComponent<ShieldScript>()!=null)
					shield_units++;
			}
			if(!returning && shield_units==0){
				hitpoints -= 10;
				if(hitpoints <= 0){
				GameScript.lives--;
				
				if(GameScript.lives<=0){
					PlayerPrefs.SetInt("highscore", GameScript.highscore); // The first is the string name that refers to the saved score, the second is your score variable (int)
					PlayerPrefs.Save();
					
					Application.LoadLevel("gameover");
				}
				hitpoints = 100;
				angle = 270;
				dist = 10;
				anglespeed = 0.0f;
				returning = true;
				GameObject s = (GameObject)Instantiate(shield,transform.position, Quaternion.identity );
				s.GetComponent<ShieldScript>().spaceshit = gameObject;
				}
				//Destroy(collider.gameObject,0.0f);
				
				StartCoroutine("TintChange");
			}
			

			//StartCoroutine("TintChange");
			
			
			
		}
		//coll.gameObject.tag = "Finish";
		//Instantiate (ball, new Vector3 (x, y, 0), Quaternion.identity);
	}

	//float horizontalSize = ;

	float barsize_x = 200.0f;
	float barsize_y = 8.0f;
	Vector2 size;
	Vector2 pos;
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public Texture2D progressBarBorder;
	public Texture2D shieldBarFull;
	public Texture2D laserBarFull;

	void OnGUI()
	{
		if (pos == Vector2.zero) {
			pos = new Vector2((Screen.width/5.0f) - barsize_x/2.0f,5);
		}
		if(size== Vector2.zero){
			size =  new Vector2(barsize_x,barsize_y);
		}
		//progressBarEmpty.Resize ((int)size.x, (int)size.y);
		// draw the background:
		GUI.DrawTexture(new Rect (pos.x-10,pos.y-3, size.x+20, size.y+20),progressBarBorder, ScaleMode.StretchToFill);
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
		
		GUI.DrawTexture(new Rect (0,0, size.x, size.y),progressBarEmpty, ScaleMode.StretchToFill);

		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * hitpoints/100, size.y));
		GUI.DrawTexture (new Rect (0, 0, size.x, size.y), progressBarFull, ScaleMode.StretchToFill);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
		// draw the background:
		//GUI.DrawTexture(new Rect (pos.x-10-18,pos.y-3, size.x, size.y),progressBarBorder, ScaleMode.StretchToFill);
		GUI.BeginGroup (new Rect (pos.x+25, pos.y+14, size.x-50, size.y-2));
		
		GUI.DrawTexture(new Rect (0,0, size.x, size.y),progressBarEmpty, ScaleMode.StretchToFill);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, (size.x-50) * shieldcd/100.0f, size.y));
		GUI.DrawTexture (new Rect (0, 0, (size.x-50), size.y), shieldBarFull, ScaleMode.StretchToFill);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
		if(lasercd>=0.0f){
		
			Vector2 pos_2 = new Vector2((Screen.width/4.0f) + 25 - barsize_x/2.0f,36);
			// draw the background:
			//GUI.DrawTexture(new Rect (pos.x-10-18,pos.y-3, size.x, size.y),progressBarBorder, ScaleMode.StretchToFill);
			GUI.BeginGroup (new Rect (pos_2.x, pos_2.y, size.x-100, size.y-2));
			
			GUI.DrawTexture(new Rect (0,0, (size.x-100), size.y),progressBarEmpty, ScaleMode.StretchToFill);
			
			// draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, (size.x-100) * lasercd/100.0f, size.y));
			GUI.DrawTexture (new Rect (0, 0, (size.x-100), size.y), laserBarFull, ScaleMode.StretchToFill);
			GUI.EndGroup ();
			
			GUI.EndGroup ();
		}
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
