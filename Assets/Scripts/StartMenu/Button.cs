using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public string nextScene;
	public AudioClip button_clic;
	public AudioClip button_hovering;
	
	protected OTSprite buttonSprite;	
	
	protected void init()
	{
		buttonSprite = GetComponent<OTSprite>();
		
		buttonSprite.onInput = onInput;
		buttonSprite.onMouseEnterOT = onMouseEnterOT;
		buttonSprite.onMouseExitOT = onMouseExitOT;
	}
	
	protected void Start()
	{
		init();
	}
	
	// Input handler
	protected virtual void onInput (OTObject owner)
	{
	    // left mouse button clicked
	    if (Input.GetMouseButtonDown(0))
	    {
			MainMenu.titleMusic.audio.PlayOneShot(button_clic);
			StartCoroutine(FadeOutSound(2));
			Fade.In(this.gameObject, "AfterFadeIn");
	    }
	}
	
	// Hovering handler
	protected void onMouseEnterOT (OTObject owner)
	{
		MainMenu.titleMusic.audio.PlayOneShot(button_hovering);
		buttonSprite.frameIndex ++;
	}
	
	protected void onMouseExitOT (OTObject owner)
	{
		buttonSprite.frameIndex --;
	}
	
	protected virtual void AfterFadeIn ()
	{
		Application.LoadLevel(nextScene);
		
		Fade.Out();
	}
	
	protected IEnumerator FadeOutSound (float seconds) 
	{	
	    float start = MainMenu.titleMusic.volume;
	    float end = 0.0f;
	    float i = 0.0f;
    	float step = 1.0f/seconds;

	    while (i <= 1.0f) 
		{
	        i += step * Time.deltaTime;
	        MainMenu.titleMusic.volume = Mathf.Lerp(start, end, i);
	        yield return new WaitForSeconds(step * Time.deltaTime);
	    }
    }
}
