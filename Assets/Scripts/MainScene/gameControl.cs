using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameControl : MonoBehaviour {
	public Transform meteorPrefab;
	private Skybox skybox;
	
	public static bool isDay;
	
	// Meteors
	public static List<Transform> meteors;
	private int nbMeteorsMax;
	private int nbMeteorsLaunched;
	private float lastMeteorFall;
	private float timePeriodMeteorFall;
	private float meteorX;
	private float meteorY;
	private float startMeteorX;
	private float endMeteorX;
	private float startMeteorY;
	private float endMeteorY;

	// Use this for initialization
	void Start () {
		meteors = new List<Transform>();
		lastMeteorFall = 0f;
		timePeriodMeteorFall = 6f;
		
		startMeteorX = -500f;
		endMeteorX = 500f;
		startMeteorY = 500f;
		endMeteorY = 750f;
		
		isDay = true;
		nbMeteorsMax = 10;
		nbMeteorsLaunched = 0;
		
		skybox = GameObject.Find("Main Camera").GetComponent<Skybox>();
	}
	
	// Update is called once per frame
	void Update () {
		// Is day or night		
		if(!isDay)
		{
			// Falling meteors management
			if(nbMeteorsLaunched < nbMeteorsMax)
			{
				if(Time.time > lastMeteorFall + timePeriodMeteorFall)
				{
					LaunchMeteor();	
					lastMeteorFall = Time.time;
					nbMeteorsLaunched ++;					
				}
			}
			else
			{
				// Back to day
				isDay = true;	
				nbMeteorsLaunched = 0;
				MainSceneGlobal.character.sleeping = false;
				skybox.material = MainSceneGlobal.day;
			}
		}
	}
	
	
	void LaunchMeteor()
	{		
		meteorX = Random.Range(startMeteorX, endMeteorX);
		meteorY = Random.Range(startMeteorY, endMeteorY);
		Transform meteor = (Transform) Instantiate(meteorPrefab, new Vector3(meteorX, meteorY, 0f), Quaternion.identity);
		meteors.Add(meteor);
	}	
	
}
