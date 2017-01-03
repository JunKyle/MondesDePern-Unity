using UnityEngine;
using System.Collections;

public class Dragon_fire : MonoBehaviour {
	// Dragon infos
	private Transform thisTransform;
	private Rigidbody thisRigidbody;
	public Transform spriteParent;
	public OTAnimatingSprite dragonSprite;
	private int direction;
	
	// Player infos
	public Transform spritePlayer;
	
	// Destination of the ray used to detect a meteor
	private float rayViewX;
	private float rayViewY;
	
	// Destination of the ray used to the fire
	private float rayMoveX;
	private float rayMoveY;
	
	// Step for the area of detection of meteor
	private float stepRayX;
	private float stepRayY;
	
	// Step for the area of collision with meteor
	private float stepFireX;	
	private float stepFireY;
	
	// Currently firing
	private bool firing;
	
	// Firing above
	private bool fire1;
	// Firing diagonaly
	private bool fire2;
	// Firing to the right or the left
	private bool fire3;
	
	// Last time the dragon
	private float lastFire;
	// Time period between two fires
	private float timePeriodFire;
	
	// Firing sound
	public AudioClip firingSound;
	// Audio read once
	private bool audioOnce;
	

	void Awake()
	{
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
		
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(0.1f);
		
		rayMoveX = 0.0f;
		rayMoveY = 0.0f;
		firing = false;
		lastFire = 0f;
		timePeriodFire = 2f;
		fire1 = false;
		fire2= false;
		fire3 = false;
		audioOnce = false;
		direction = 1;
		
		stepRayX = 10f;
		stepRayY = 10f;
		stepFireX = 10f;
		stepFireY = 10f;
	}
	
	// Update is called once per frame
//	void Update () {
//		// Draw a line renderer 
//		// at the 0 position with 0 width
//		LineRenderer lr = gameObject.GetComponent<LineRenderer>();
//		lr.SetPosition(0, thisTransform.position);
//		lr.SetWidth(0f, 0f);
//		
//		// Detect direction of the dragon
//		/*float diff = spritePlayer.position.x - thisTransform.position.x;
//		if(diff > 0)
//		{
//			
//			direction = 1;
//		}
//		else
//		{
//			direction = -1;
//		}*/	
//		
//		// Detect meteore in the three directions
//		if(Detection(0f, 250f, stepRayX, 0f)) // ABOVE
//		{
//			firing = true;
//			fire1 = true;
//			fire2= false;
//			fire3 = false;	
//			stepRayX += 10f;
//		}
//		/*else if(Detection(500f, 500f, 0f, 10f)) // DIAGONALY
//		{
//			firing = true;
//			fire1 = false;
//			fire2= true;
//			fire3 = false;	
//		}*/
//		else if(Detection(250f, 0f, 0f, stepRayY)) // RIGHT OR LEFT
//		{
//			firing = true;
//			fire1 = false;
//			fire2= false;
//			fire3 = true;
//			stepRayY += 10f;
//		}
//		
//		// If the dragon stop firing
//		if(!firing)
//		{
//			// Redraw the Line Renderer of the fire 
//			// at the first position
//			lr.SetPosition(1, thisTransform.position);
//			rayMoveX = 0.5f;
//			rayMoveY = 0.5f;
//			
//			// False in order to re-read the audio
//			audioOnce = false;
//		}
//		else
//		{
//			// The dragon must fire
//			
//			// Above
//			if(fire1)
//			{
//				Fire (0f, 10f, stepFireX, 0f);
//				stepFireX += 10f;
//				dragonSprite.PlayLoop("Firing_3");	
//			}
//			// Diagonaly
//			else if(fire2)
//			{
//				Fire (50f, 50f, 0f, stepFireY);
//				stepFireY += 10f;
//				dragonSprite.PlayLoop("Firing_2");	
//			}
//			// Right or left
//			else if(fire3)
//			{
//				Fire (10f, 0f, 0f, stepFireY);	
//				stepFireY += 10f;
//				dragonSprite.PlayLoop("Firing_1");					
//			}
//			
//			// Audio still not read
//			if(!audioOnce)
//			{
//				AudioSource music = GameObject.Find("Background music").GetComponent<AudioSource>();
//				music.audio.PlayOneShot(firingSound);
//				audioOnce = true;
//			}
//		}
//		
//		// Check if the time period between two fires has gone by
//		if(Time.time > lastFire + timePeriodFire)
//		{
//			// Reinit the fire boolean
//			firing = false;	
//			fire1 = false;
//			fire2= false;
//			fire3 = false;
//			
//			// Reinit the last time fire
//			lastFire = Time.time;
//		}
//		
//		initStepRay();
//	}
	
	void initStepRay()
	{
		if(stepRayX >= 1000f)
		{
			stepRayX = 10f;	
		}
		if(stepRayY >= 1000f)
		{
			stepRayY = 10f;	
		}
		if(stepFireX >= 1000f)
		{
			stepFireX = 10f;	
		}
		if(stepFireY >= 1000f)
		{
			stepFireY = 10f;	
		}
	}
	
	/// <summary>
	/// Detect if a meteor is the area you're looking.
	/// </summary>
	/// <param name='_rayViewX'>
	/// X of the ray used to detect a meteor
	/// </param>
	/// <param name='_rayViewY'>
	/// Y of the ray used to detect a meteor
	/// </param>
	/// <param name='_rayViewInternX'>
	/// Step x of the area you want to detect
	/// </param>
	/// <param name='_rayViewInternY'>
	/// Step Y of the area you want to detect
	/// </param>
	public bool Detection(float _rayViewX, float _rayViewY, float _rayViewInternX, float _rayViewInternY)
	{
		// Start position for the detection area
		float rayViewInternY = _rayViewY;
		float rayViewInternX = _rayViewX;
		
		// Position of the ray direction
		rayViewX = _rayViewX;
		rayViewY = _rayViewY;		
		
		RaycastHit obj = new RaycastHit();
		float goalX = thisTransform.position.x - rayViewX;
		float goalY = thisTransform.position.y + rayViewY;
		if(Physics.Raycast(thisTransform.position, new Vector3(goalX - rayViewInternX, goalY - rayViewInternY, 0f), out obj))
		{				
			if(obj.transform.gameObject.name == "Meteor(Clone)")
			{
				Debug.Log("MUST FIRE");
				// Meteor detected
				return true;
			}
		}
		// No meteor detected
		return false;
	}
	
	/// <summary>
	/// Fire and detect collision with a meteor (destruction of the meteor)
	/// </summary>
	/// <param name='moveX'>
	/// Position X to reach
	/// </param>
	/// <param name='moveY'>
	/// Position Y to reach
	/// </param>
	/// <param name='_rayViewInternX'>
	/// Step x of the area you want to detect
	/// </param>
	/// <param name='_rayViewInternY'>
	/// Step Y of the area you want to detect
	/// </param>
	void Fire(float moveX, float moveY, float _rayViewInternX, float _rayViewInternY)
	{
		// Start position for the detection area
		float rayViewInternY = rayViewY;
		float rayViewInternX = rayViewX;
		
		// Detect collision with a meteor
		RaycastHit obj = new RaycastHit();
		float goalX = thisTransform.position.x - rayMoveX;
		float goalY = thisTransform.position.y + rayMoveY;
		
		// Draw the thread of fire with the (rayMoveX; rayMoveY) as point to reach
		LineRenderer lr = gameObject.GetComponent<LineRenderer>();
		lr.SetPosition(1, new Vector3(goalX, goalY, 0f));
		lr.SetWidth(100f, 100f);
		
		if(Physics.Raycast(thisTransform.position, new Vector3(goalX - rayViewInternX, goalY - rayViewInternY, 0f), out obj))
		{
			if(obj.transform.gameObject.name == "Meteor(Clone)")
			{
				gameControl.meteors.Remove(obj.transform);
				Destroy(obj.transform.gameObject);
				firing = false;
			}
		}
		// Increment the step X for the point to reach with the fire
		rayMoveX += moveX;
		// Increment the step Y for the point to reach with the fire
		rayMoveY += moveY;
	}
}
