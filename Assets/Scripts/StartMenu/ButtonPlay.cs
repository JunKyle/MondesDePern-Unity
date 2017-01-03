using UnityEngine;

public class ButtonPlay : Button
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
		MainMenu.backgroundStory.SetActive(true);
        MainMenu.buttonNormal.SetActive(true);
		MainMenu.buttonChallenge.SetActive(true);
		
		MainMenu.backgroundCredits.SetActive(false);
		MainMenu.buttonBack.SetActive(false);
		
		MainMenu.backgroundTitle.SetActive(false);
        MainMenu.buttonPlay.SetActive(false);
		MainMenu.buttonCredits.SetActive(false);
		MainMenu.buttonQuit.SetActive(false);
		
		Fade.Out();
	}
}


