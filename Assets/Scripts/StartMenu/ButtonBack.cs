using UnityEngine;

public class ButtonBack : Button
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
		
		MainMenu.backgroundCredits.SetActive(false);
		MainMenu.buttonBack.SetActive(false);
		
		MainMenu.backgroundTitle.SetActive(true);
        MainMenu.buttonPlay.SetActive(true);
		MainMenu.buttonCredits.SetActive(true);
		MainMenu.buttonQuit.SetActive(true);
		
		Fade.Out();
	}
}