using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {
	private Transform thisTransform;
	private Rigidbody thisRigidbody;
	public Transform spriteParent;
	public OTAnimatingSprite meteoriteSprite;
	public bool falling = true;
	private Vector3 actualPosition;
	private Vector3 startPosition;
	private int sens;
	public AudioClip hittingSound;
	
	void Awake()
	{
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
	
	// Use this for initialization
	void Start()
	{
		
		ResetMeteor();
	}
	
	// Update is called once per frame
	void Update () {
		if(falling)
		{
			// Falling meteor
			thisTransform.rigidbody.velocity = new Vector3(sens*50,-100f,0);
			actualPosition = thisTransform.position;
			
			meteoriteSprite.PlayLoop("burn");
			
			LineRenderer lr = gameObject.GetComponent<LineRenderer>();
			lr.SetPosition(0, startPosition);
			lr.SetPosition(1, actualPosition);
			lr.SetWidth(50f, 50f);
		}
		else{
			thisTransform.position = actualPosition;
			meteoriteSprite.Stop();
			Destroy(gameObject.GetComponent<LineRenderer>());
			OT.DestroyObject(meteoriteSprite);
		}
	}
	
	public void ResetMeteor()
	{
		sens = Random.Range(0, 100);
		if(sens >= 50)
		{
			sens = 1;	
		}
		else
		{
			sens = -1;	
		}
		
		thisTransform.parent = null;
		
		thisRigidbody.isKinematic = false;
		thisRigidbody.useGravity = true;
		
		startPosition = thisTransform.position;
		
		falling = true;
	}
	
	public void OnTriggerStay(Collider other)
	{
		if(other.gameObject.layer == 8)
		{
			falling = false;
			AudioSource music = GameObject.Find("Block sound").GetComponent<AudioSource>();
			music.audio.PlayOneShot(hittingSound);
			Burn(other.gameObject);
		}
	}
	
	public void Burn(GameObject bloc)
	{
		OT.DestroyObject(bloc);	
	}
}
