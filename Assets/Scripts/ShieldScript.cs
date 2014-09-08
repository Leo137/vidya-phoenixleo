using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	public GameObject spaceshit;

	// Use this for initialization
	void Start () {
		transform.position = spaceshit.transform.position;
		transform.localScale = spaceshit.transform.localScale;
		Invoke ("Expiring", 3.5f);
		Invoke ("DestroyShield", 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = spaceshit.transform.position;
		transform.localScale = spaceshit.transform.localScale;

	}


	
	void Expiring(){
		StartCoroutine("TintChange");
	}

	void DestroyShield(){
		Destroy (gameObject, 0.0f);
	}

	IEnumerator TintChange(){
		int x = 0;
		for (x=0; x<400; x++) {
			gameObject.renderer.material.color = Color.yellow;
			yield return new WaitForSeconds (0.02f);
			gameObject.renderer.material.color = Color.black;
			yield return new WaitForSeconds (0.02f);
			gameObject.renderer.material.color = Color.white;
			yield return new WaitForSeconds (0.02f);
		}
	}
}
