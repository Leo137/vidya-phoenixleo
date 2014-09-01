using UnityEngine;
using System.Collections;

public class OptionsArrowScript2 : MonoBehaviour {
	

	public GUIText[] letras;

	public GUIText end;
	public GUIText score;

	int options; 
	int letra;

	bool m_isAxisInUse = false;
	bool m2_isAxisInUse = false;
	// Use this for initialization
	void Start () {
		options = 0;
		letra = 1;
		end.enabled = false;

		score.text = PlayerPrefs.GetInt ("highscore").ToString ();

	}
	
	// Update is called once per frame
	void Update () {

		
		if( Input.GetAxis ("Vertical") < 0)
		{
			if(m_isAxisInUse == false)
			{
				if(letra<=3&&letras[letra-1].text.ToCharArray()[0] < 'Z')
					letras[letra-1].text = char.ConvertFromUtf32(letras[letra-1].text.ToCharArray()[0] + (char)1);

			}
			m_isAxisInUse = true;
		}

		
		
		if( Input.GetAxis ("Vertical") > 0)
		{
			if(m2_isAxisInUse == false)
			{
				if(letra<=3&&letras[letra-1].text.ToCharArray()[0] > 'A')
					letras[letra-1].text = char.ConvertFromUtf32(letras[letra-1].text.ToCharArray()[0] - (char)1);


			}
			m2_isAxisInUse = true;
		}

		if(letra>3){
			end.enabled = true;
		}
		else{
			end.enabled = false;
		}
		


		if( Input.GetAxis ("Vertical") == 0)
		{
			m_isAxisInUse = false;
			m2_isAxisInUse = false;
		} 
		
		
		Vector3 position_2 = transform.position;
		
		position_2.y = (0.275f - options*0.050f) - 0.025f;
		position_2.x = 0.390f + (letra-1)*0.15f;
		
		transform.position = position_2;

		if (Input.GetButtonDown ("Fire1")) {
			
			if(letra>3){
				PlayerPrefs.SetString("name",letras[0].text+letras[1].text+letras[2].text); // The first is the string name that refers to the saved score, the second is your score variable (int)
				PlayerPrefs.Save();
				Application.LoadLevel("mainscreen");
			}
			letra++;
		}
		
		if (Input.GetButtonDown ("Fire2")) {
			
			if(letra>1){
				letra--;
			}
		}
	}
}
