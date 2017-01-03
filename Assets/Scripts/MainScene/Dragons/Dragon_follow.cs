using UnityEngine;
using System.Collections;

public class Dragon_follow : MonoBehaviour {
	// Dragon infos
	private Transform thisTransform;
	private Rigidbody thisRigidbody;
	public Transform spriteParent;
	public OTAnimatingSprite dragonSprite;
	// Dragon turned right
	private bool right;
	// Dragon tamed
	
	
	// Player infos
	public Transform spritePlayer;	
	private Vector3 playerMove;
	private Vector3 startPlayerPosition;
	private Vector3 endPlayerPosition;
	
	// Flying sound
	public AudioClip flyingSound;
	// Audio read once
	private bool audioOnce;
	
	// Boolean for the follow
	private bool followPlayer;
	
	[HideInInspector] public bool firing;
	[HideInInspector] public bool isTamed;
	[HideInInspector] public bool grounded;
	
	void Awake()
	{
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
		
	
	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(0.1f);
		playerMove.x = 0f;
		playerMove.y = 0f;
		followPlayer = false;
		grounded = false;
		
		startPlayerPosition = spritePlayer.position;
		right = false;
		isTamed = false;
		
		dragonSprite.onInput = onInput;
		
		dragonSprite.Play("Sleeping");
	}
	
	// Update is called once per frame
	void Update () {
				
		if(!firing && followPlayer && isTamed)
		{
			Follow();	
		}
		
		//if (!grounded && isTamed)
		//	dragonSprite.PlayLoop("Flying");
		
		if (grounded && isTamed && !followPlayer && !firing)
			dragonSprite.PlayLoop("Sitting");
				
		endPlayerPosition = spritePlayer.position;
	}
	
	// Input handler
	protected virtual void onInput (OTObject owner)
	{
	    // left mouse button clicked
	    if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
	    {
			float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
			if (distance < 250)
			{
				if (isTamed){
					followPlayer = !followPlayer;
				}				
				else if(Inventory.currentBlock != null && Inventory.currentBlock.TypeID == "fruit"){
					MainSceneGlobal.character.feeding = true;
					StartCoroutine("taming");
				}
					
			}
	    }
	}
	
	IEnumerator taming(){
		
		yield return new WaitForSeconds(OT.AnimationByName("Dwarf Animation").GetFrameset("Feeding").singleDuration);
		
		isTamed = true;
		
		dragonSprite.Play("Sitting");
		
		MainSceneGlobal.character.feeding = false;
	}
	
	public void Follow()
	{
		// Animation played
		if(playerMove.x == 0f && playerMove.y == 0f)
		{
			dragonSprite.PlayLoop("Sitting");			
		}
		else
		{
			dragonSprite.PlayLoop("Flying");
		}
		
		// Face direction
		float diff = spritePlayer.position.x - thisTransform.position.x;
		// Change direction depending of the position of the player
		if(diff > 0)
		{
			// Right
			if(!right)
			{
				// Rotate
				Vector3 v = new Vector3(thisTransform.localScale.x * -1, thisTransform.localScale.y, thisTransform.localScale.z);
				thisTransform.localScale = v; 
				right = true;
			}
		}
		else
		{
			// Left
			if(right)
			{
				// Rotate
				Vector3 v = new Vector3(thisTransform.localScale.x * -1, thisTransform.localScale.y, thisTransform.localScale.z);
				thisTransform.localScale = v; 
				right = false;
			}
		}	
		
		// Change position of the dragon
		if(Mathf.Abs(diff) >= 200)
		{
			thisTransform.position += new Vector3(playerMove.x, 0f, 0f);			
		}
		thisTransform.position += new Vector3(0f, playerMove.y, 0f);
		
		// Get player infos position
		endPlayerPosition = spritePlayer.position;		
		playerMove.x = (endPlayerPosition.x - startPlayerPosition.x);
		playerMove.y = (endPlayerPosition.y - startPlayerPosition.y);
		startPlayerPosition = spritePlayer.position;	
	}
	
	public void OnTriggerStay(Collider other)
	{
		// Collider is type of Ground
		if(other.gameObject.layer == 8)
		{
			thisTransform.rigidbody.velocity = new Vector3(0f, 0f, 0f);
			grounded = true;
		}
	}
	
	public void OnTriggerExit(Collider other)
	{
		// Collider is type of Ground
		if(other.gameObject.layer == 8)
		{
			thisTransform.rigidbody.velocity = new Vector3(0f, 20f, 0f);
			grounded = false;
		}
	}
	
}

