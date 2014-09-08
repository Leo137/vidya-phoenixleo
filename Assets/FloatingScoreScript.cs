using UnityEngine;
using System.Collections;

public class FloatingScoreScript : MonoBehaviour {
	
	public GUIText guitext;
	// Use this for initialization
	void Start () {
		Invoke ("Die",2.0f);
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position +=  new Vector3(0,0.001f,0);
		guitext.material.color = new Color(guitext.material.color.r,guitext.material.color.g,guitext.material.color.b,
		                                   guitext.material.color.a*0.98f);
	}
	
	void Die(){
		Destroy(gameObject,0.0f);
	}
}
