using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public static GameObject backgroundTitle;
	public static GameObject backgroundStory;
	public static GameObject backgroundCredits;
	public static GameObject buttonPlay;
	public static GameObject buttonQuit;
	public static GameObject buttonCredits;
	public static GameObject buttonNormal;
	public static GameObject buttonChallenge;
	public static GameObject buttonBack;
	
	public static AudioSource titleMusic;
	
	private void init()
	{
		backgroundTitle = GameObject.Find("Background_Title");
		backgroundStory = GameObject.Find("Background_Story");
		backgroundCredits = GameObject.Find("Background_Credits");
		buttonPlay = GameObject.Find("Button play");
		buttonQuit = GameObject.Find("Button quit");
		buttonCredits = GameObject.Find("Button credits");
		buttonNormal = GameObject.Find("Button normal");
		buttonChallenge = GameObject.Find("Button challenge");
		buttonBack = GameObject.Find("Button back");
		
		titleMusic = GameObject.Find("Music").GetComponent<AudioSource>();
	}
	
	public void Start()
	{
		init();
		
		backgroundStory.SetActive(false);
        buttonNormal.SetActive(false);
		buttonChallenge.SetActive(false);
		
		backgroundCredits.SetActive(false);
		buttonBack.SetActive(false);
		
		backgroundTitle.SetActive(true);
        buttonPlay.SetActive(true);
		buttonCredits.SetActive(true);
		buttonQuit.SetActive(true);
	}
}

