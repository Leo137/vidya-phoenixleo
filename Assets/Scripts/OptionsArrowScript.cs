using UnityEngine;
using System.Collections;

public class OptionsArrowScript : MonoBehaviour {

	int options = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool m_isAxisInUse = false;
		bool m2_isAxisInUse = false;

		if( Input.GetAxis ("Vertical") < 0)
		{
			if(m_isAxisInUse == false)
			{
				if(options < 1)
					options++;
				m_isAxisInUse = true;
			}
		}
		if( Input.GetAxis ("Vertical") == 0)
		{
			m_isAxisInUse = false;
			m2_isAxisInUse = false;
		} 


		if( Input.GetAxis ("Vertical") > 0)
		{
			if(m2_isAxisInUse == false)
			{
				if(options > 0)
					options--;
				m2_isAxisInUse = true;
			}
		}

		if (Input.GetButton ("Fire1")) {
			if(options==0)Application.LoadLevel("gameplay");
			if(options==1)Application.LoadLevel("credits");
		}


		Vector3 position_2 = transform.position;

		position_2.y = (0.275f - options * 0.075f);
		position_2.x = 0.400f;

		transform.position = position_2;
	}
}
