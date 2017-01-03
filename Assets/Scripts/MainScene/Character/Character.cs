using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	private Vector3 vel2;
	private Vector3 vel;
	private float jumpVel = 350f;
	private float moveVel = 150f;
	private float gravityY;
	private float maxVelY = 0f;
	private float fallVel = 400f;
	private int jumps = 0;
	
	private float halfMyX;
	private float halfMyY;
	
	private float rayLenghtX;
	private float rayLenghtY;
	
	[HideInInspector] public enum facing { Right, Left }
	[HideInInspector] public facing facingDir;
		
	[HideInInspector] public bool isLeft;
	[HideInInspector] public bool isRight;
	[HideInInspector] public bool isJump;
	[HideInInspector] public bool isPass;
	[HideInInspector] public bool grounded;
	[HideInInspector] public bool jumping;
	[HideInInspector] public bool hitting;
	[HideInInspector] public bool feeding;
	[HideInInspector] public bool sleeping;
	
	// layer masks
	protected int groundMask = 1 << 9 | 1 << 8; // Ground, Block
	protected int blockMask = 1 << 8; //Block

	// Use this for initialization
	void Start () 
	{
		isJump = false;
		isPass = false;
		isLeft = false;
		isRight = false;
		grounded = false;
		jumping = false;
		feeding = false;
		sleeping = false;
		
		vel.y = 0;
		maxVelY = fallVel;
		
		halfMyX = transform.localScale.x/2;
		halfMyY = transform.localScale.y/2;
		
		rayLenghtX = halfMyX + 4.0f;
		rayLenghtY = halfMyY + 4.0f;
		
		StartCoroutine(StartGravity());	
	}
	
	IEnumerator StartGravity()
	{
		// wait for things to settle before applying gravity
		yield return new WaitForSeconds(0.1f);
		gravityY = 500f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		isJump = false;
		isPass = false;
		isLeft = false;
		isRight = false;
		
		if (Input.GetAxis("Horizontal") > 0)
		{
			if (!sleeping){
				isRight = true;
				facingDir = facing.Right;
			}
			
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			if (!sleeping){
				isLeft = true;
				facingDir = facing.Left;
			}			
		}
		else
		{
			isLeft = false;
			isRight = false;
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) 
		{ 
			isJump = true;
			jumping = true;
			grounded = false;
			jumps++;
			
			if (jumps == 1)
				vel.y = jumpVel;
		}
		
		if (!sleeping && !feeding ){
			vel.x = Input.GetAxis("Horizontal")*moveVel;
		}
		
		// landed from fall/jump
		if(grounded == true && vel.y == 0)
		{
			jumping = false;
			jumps = 0;
		}
				
		updateRaycasts();
		
		// apply gravity while airborne
		if(grounded == false)
		{
			vel.y -= gravityY * Time.deltaTime;
		}
		
		// velocity limiter
		if(vel.y < -maxVelY)
		{
			vel.y = -maxVelY;
		}
		
		// apply movement 
		vel2 = vel * Time.deltaTime;
		transform.position += new Vector3(vel2.x,vel2.y,0f);
	
	}
	
	void updateRaycasts(){
		RaycastHit hitInfo;
		grounded = false;		
		
		float absVel2X = Mathf.Abs(vel2.x);
		float absVel2Y = Mathf.Abs(vel2.y);
		
		if (Physics.Raycast(new Vector3(transform.position.x-halfMyX/2,transform.position.y,transform.position.z), -Vector3.up, out hitInfo, rayLenghtY+absVel2Y, groundMask) 
			|| Physics.Raycast(new Vector3(transform.position.x+halfMyX/2,transform.position.y,transform.position.z), -Vector3.up, out hitInfo, rayLenghtY+absVel2Y, groundMask))
		{			
			// not while jumping so he can pass up thru platforms
			if(vel.y <= 0)
			{
				grounded = true;
				vel.y = 0f; // stop falling			
				transform.position = new Vector3(transform.position.x,hitInfo.point.y+halfMyY,0f);
			}
		}
		
		// blocked up
		if (Physics.Raycast(new Vector3(transform.position.x-halfMyX/2,transform.position.y,transform.position.z), Vector3.up, out hitInfo, rayLenghtY+absVel2Y, groundMask)
			|| Physics.Raycast(new Vector3(transform.position.x+halfMyX/2,transform.position.y,transform.position.z), Vector3.up, out hitInfo, rayLenghtY+absVel2Y, groundMask))
		{
			if(vel.y > 0)
				vel.y = 0f;
		}
		
		// blocked on right
		if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+halfMyY/2,transform.position.z), Vector3.right, out hitInfo, rayLenghtX+absVel2X, groundMask)
			|| Physics.Raycast(new Vector3(transform.position.x,transform.position.y-halfMyY/2,transform.position.z), Vector3.right, out hitInfo, rayLenghtX+absVel2X, groundMask))
		{
			if(facingDir == facing.Right)
			{
				vel.x = 0f;
				transform.position = new Vector3(hitInfo.point.x-(halfMyX-0.01f),transform.position.y, 0f); // .01 less than collision width.
			}
			
		}
		
		// blocked on left
		if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y+halfMyY/2,transform.position.z), -Vector3.right, out hitInfo, rayLenghtX+absVel2X, groundMask)
			|| Physics.Raycast(new Vector3(transform.position.x,transform.position.y-halfMyY/2,transform.position.z), -Vector3.right, out hitInfo, rayLenghtX+absVel2X, groundMask))
		{
			if(facingDir == facing.Left){
				vel.x = 0f;
				transform.position = new Vector3(hitInfo.point.x+(halfMyX-0.01f),transform.position.y, 0f); // .01 less than collision width.
			}
			
		}
	}
}
