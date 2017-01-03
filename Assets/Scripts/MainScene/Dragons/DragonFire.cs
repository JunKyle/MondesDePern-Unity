using UnityEngine;
using System.Collections;

public class DragonFire : MonoBehaviour {
	
	private Dragon_follow dragon;
	private GameObject meteor;
	public GameObject dragonParent;
	private LineRenderer lineRenderer;
	
	public Transform spriteParent;
	private Vector3 spriteParentLocalScale;
	
	private Vector3 firePos;
	private Vector3 goal;
	
	private Vector3 fireStep;
	
	private bool isFiring;
	
	// Firing sound
	public AudioClip firingSound;

	// Use this for initialization
	void Start () {
		spriteParentLocalScale = spriteParent.localScale;
		
		dragon = (Dragon_follow) dragonParent.GetComponent<Dragon_follow>();
		lineRenderer = dragonParent.GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, dragonParent.transform.position);
		lineRenderer.SetWidth(0f, 0f);
		
		firePos = dragonParent.transform.position;
		fireStep = new Vector3(10f, 10f, 0);
		
		isFiring = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (dragon.firing && dragon.isTamed && dragon.grounded){
			//firePos = firePos + fireStep;
			if (!isFiring){
				isFiring = true;
				StartCoroutine("fire");
			}
		}
		
	}
	
	IEnumerator fire()
	{
		Vector3 diff = goal - dragonParent.transform.position;
			
		if (Mathf.Abs(diff.x) < 100){
			dragon.dragonSprite.PlayOnce("Firing_3");
		}
		
		//Meteor is his left
		else if (diff.x < 0){
			
			if (diff.y > 150){
				dragon.dragonSprite.PlayOnce("Firing_2");
			}
			else
				dragon.dragonSprite.PlayOnce("Firing_1");
		}
		else{
			if (diff.y > 150){
				spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
				dragon.dragonSprite.PlayOnce("Firing_2");
			}
			else{
				spriteParent.localScale = new Vector3(-spriteParentLocalScale.x, spriteParentLocalScale.y, spriteParentLocalScale.z);
				dragon.dragonSprite.PlayOnce("Firing_1");
			}
				
		}
		
		AudioSource music = GameObject.Find("Block sound").GetComponent<AudioSource>();
		music.audio.PlayOneShot(firingSound);
		
		lineRenderer.SetPosition(1, goal);
		lineRenderer.SetWidth(100f, 100f);
		
		OT.DestroyObject(meteor);
		
		yield return new WaitForSeconds(1);
		
		dragon.firing = false;
		isFiring = false;
		lineRenderer.SetPosition(0, dragonParent.transform.position);
		lineRenderer.SetWidth(0f, 0f);
	}
	
	void OnTriggerEnter(Collider other) {
		
		//Meteor enter in detection zone
		if (other.gameObject.layer == 9){
			dragon.firing = true;
			goal = other.transform.position;
			meteor = other.gameObject;
		}
		
	}
	
}
