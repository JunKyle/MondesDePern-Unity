using UnityEngine;
using System.Collections;

public class MainSceneGlobal : MonoBehaviour {
	
	public static Character character;
	public static Material day;
	public static Material night;
	
	private UIDialog dialog;
	
	// Use this for initialization
	void Start () {
		character = GameObject.Find("Dwarf").GetComponent<Character>();
		day = (Material)Resources.Load("SkyboxDay");
		night = (Material)Resources.Load("SkyboxNight");
		
		Rect pos = new Rect (Screen.width/2 - 150, Screen.height/2 - 100, 300, 200);
		dialog = new UIDialog (	pos, "Warning", 
								"Retourner à l'écran titre ?", 
								delegate() {
									Application.LoadLevel("StartMenu");												
								}, null);
		
		dialog.Show = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			dialog.Show = true;
		}
	}
	
	void OnGUI()
	{
		dialog.showDialog();
	}
}
