using UnityEngine;
using System.Collections;

public class CraftingTable : MonoBehaviour 
{
	private OTSprite sprite;
	private AudioSource music;
	private bool paused;
	
	// Use this for initialization
	void Start () 
	{
		sprite = GetComponent<OTSprite>();	
		sprite.onInput = onInput;
		
		music = GameObject.Find("Background music").GetComponent<AudioSource>();
		paused = false;
	}
	
	protected virtual void onInput (OTObject owner)
	{
	    // right mouse button clicked
	    if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
	    {
			float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
			if (distance < 150)
			{
				if (!paused)
				{
					paused = true;
					music.Pause();
				}
				else
				{
					paused = false;
					music.Play();
				}					
			}
	    }
	}
}
