using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
	public enum anim { None, Sleeping, WalkLeft, WalkRight, FallLeft, FallRight, HittingLeft, HittingRight, FeedingLeft, FeedingRight}
	
	public Transform spriteParent;
	public OTAnimatingSprite playerSprite;
	
	private Character character;
	private Vector3 spriteParentLocalScale;
	
	private anim currentAnim;

	// Use this for initialization
	void Start () {
		character = GetComponent<Character>();
		spriteParentLocalScale = spriteParent.localScale;
		currentAnim = anim.None;
		playerSprite.Play("Idle");
		playerSprite.onAnimationFinish = OnAnimationFinish;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!character.hitting && !character.sleeping && !character.feeding && character.isLeft && currentAnim != anim.WalkLeft && character.grounded){
			currentAnim = anim.WalkLeft;
			playerSprite.Play("Walking");
			spriteParent.localScale = new Vector3(spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (!character.hitting && !character.sleeping && !character.feeding && character.isRight && currentAnim != anim.WalkRight && character.grounded){
			currentAnim = anim.WalkRight;
			playerSprite.Play("Walking");
			spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (!character.isLeft && !character.isRight && !character.hitting 
			&& !character.sleeping && !character.feeding
			&& currentAnim != anim.None && character.grounded)
		{
			currentAnim = anim.None;
			playerSprite.Play("Idle");
			if (character.facingDir == Character.facing.Right)
				spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			else
				spriteParent.localScale = new Vector3(spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (!character.grounded && !character.hitting && currentAnim != anim.FallLeft && character.facingDir == Character.facing.Left){
			currentAnim = anim.FallLeft;
			playerSprite.Play("Jumping");
			spriteParent.localScale = new Vector3(spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (!character.grounded && !character.hitting && currentAnim != anim.FallRight && character.facingDir == Character.facing.Right){
			currentAnim = anim.FallRight;
			playerSprite.Play("Jumping");
			spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (character.hitting && currentAnim != anim.HittingLeft && character.facingDir == Character.facing.Left){
			currentAnim = anim.HittingLeft;
			playerSprite.PlayOnce("Hitting");
			spriteParent.localScale = new Vector3(spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (character.hitting && currentAnim != anim.HittingRight && character.facingDir == Character.facing.Right){
			currentAnim = anim.HittingRight;
			playerSprite.PlayOnce("Hitting");
			spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (character.feeding && currentAnim != anim.FeedingLeft && character.facingDir == Character.facing.Left){
			currentAnim = anim.FeedingLeft;
			playerSprite.Play("Feeding");
			spriteParent.localScale = new Vector3(spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (character.feeding && currentAnim != anim.FeedingRight && character.facingDir == Character.facing.Right){
			currentAnim = anim.FeedingRight;
			playerSprite.Play("Feeding");
			spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
		
		if (character.sleeping && currentAnim != anim.Sleeping){
			currentAnim = anim.Sleeping;
			playerSprite.Play("Sleeping");
			spriteParent.localScale = new Vector3(spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
			return;
		}
			
	}
	
	public void OnAnimationFinish(OTObject owner)
	{
		if (character.hitting){
			character.hitting = false;
			playerSprite.looping = true;
		}
		
		if (character.feeding){
			character.feeding = false;
			playerSprite.looping = true;
		}
			
	}
}
