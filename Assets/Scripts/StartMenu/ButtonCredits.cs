using UnityEngine;

public class ButtonCredits : Button
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
		MainMenu.backgroundStory.SetActive(false);
        MainMenu.buttonNormal.SetActive(false);
		MainMenu.buttonChallenge.SetActive(false);
		
		MainMenu.backgroundCredits.SetActive(true);
		MainMenu.buttonBack.SetActive(true);
		
		MainMenu.backgroundTitle.SetActive(false);
        MainMenu.buttonPlay.SetActive(false);
		MainMenu.buttonCredits.SetActive(false);
		MainMenu.buttonQuit.SetActive(false);
		
		Fade.Out();
	}
}



