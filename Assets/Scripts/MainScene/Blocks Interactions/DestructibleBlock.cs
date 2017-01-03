using UnityEngine;
using System.Collections;

public class DestructibleBlock : MonoBehaviour {

	protected OTSprite sprite;
	public OTSprite Sprite {get; set;}
	protected int initialCollisionDepth;
	
	public AudioClip hittingSound;
	public int blockStamina = 3;
	public Texture inventoryIcon;
	public int nbDrop = 1;
	public bool restorable = true;
	public string typeID;
	
	protected int currentStamina;
	
	protected bool mouseOnBlock;
	
	protected enum BlockState {Normal, Damaged, Destroyed, TakingDmg}
	protected BlockState state;
	
	protected OTAnimatingSprite playerSprite;

	// Use this for initialization
	void Start () {	
		
		playerSprite = OT.AnimatingSprite("Dwarf Sprite");
		sprite = GetComponent<OTSprite>();
		
		sprite.onMouseEnterOT = onMouseEnterOT;
		sprite.onMouseExitOT = onMouseExitOT;
		
		currentStamina = blockStamina;
		
		mouseOnBlock = false;
		state = BlockState.Normal;
		
		initialCollisionDepth = sprite.collisionDepth;
	}
		
	// Input handler
	protected virtual void onMouseEnterOT (OTObject owner)
	{		
		mouseOnBlock = true;
	}
	
	// Input handler
	protected virtual void onMouseExitOT (OTObject owner)
	{
		//Block still not broken
		if (sprite.visible){
			currentStamina = blockStamina;
			state = BlockState.Normal;
		}
		
		MainSceneGlobal.character.hitting = false;
		playerSprite.looping = true;
		mouseOnBlock = false;
	}
	
	protected IEnumerator damageBlock(){
		
		if (currentStamina > 0){
			AudioSource music = GameObject.Find("Block sound").GetComponent<AudioSource>();
			music.audio.PlayOneShot(hittingSound);
			
			state = BlockState.TakingDmg;
			
			yield return new WaitForSeconds(OT.AnimationByName("Dwarf Animation").GetFrameset("Hitting").singleDuration);
			
			currentStamina -= Player.pickaxeStrenght;
			
			if (currentStamina <= 0) {
				destroyBlock();
				MainSceneGlobal.character.hitting = false;
				playerSprite.looping = true;
			}
			else
				state = BlockState.Damaged;
		}
	}
	
	public void destroyBlock(){
		state = BlockState.Destroyed;
		
		for (int i = 0; i < nbDrop; ++i)
		{
			Player.addItem(new Item (typeID, inventoryIcon, restorable));
		}
		
		OT.DestroyObject(sprite);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// left mouse button clicked
	    if (mouseOnBlock && Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
	    {
			//Debug.Log(this.name + "clicked health : " + currentStamina);
			float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
			if (distance < 150)
			{
				switch(state){
				case BlockState.Normal:
					MainSceneGlobal.character.hitting = true;
					StartCoroutine("damageBlock");
					break;
				case BlockState.Damaged:
					MainSceneGlobal.character.hitting = true;
					StartCoroutine("damageBlock");
					break;
				}
			}
	    }			
	}
}
