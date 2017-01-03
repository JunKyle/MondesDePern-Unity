using UnityEngine;

public class ButtonQuit : Button
{
	// Input handler
	override protected void onInput (OTObject owner)
	{
	    // left mouse button clicked
	    if (Input.GetMouseButtonDown(0))
	    {	
			MainMenu.titleMusic.audio.PlayOneShot(button_clic);
			Fade.In(this.gameObject, "AfterFadeIn");
	    }
	}
	
	override protected void AfterFadeIn ()
	{
		Application.Quit();
	}
}

