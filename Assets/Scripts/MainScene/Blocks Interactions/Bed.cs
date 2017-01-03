using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour 
{
	private OTSprite sprite;
	private Skybox skybox;
	
	// Use this for initialization
	void Start () 
	{
		sprite = GetComponent<OTSprite>();
		sprite.onInput = onInput;
		
		skybox = GameObject.Find("Main Camera").GetComponent<Skybox>();
	}
	
	protected virtual void onInput (OTObject owner)
	{
	    // right mouse button clicked
	    if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
	    {
			float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
			if (distance < 150)
			{
				gameControl.isDay = false;
				skybox.material = MainSceneGlobal.night;
				MainSceneGlobal.character.sleeping = true;
			}
	    }
	}
}
