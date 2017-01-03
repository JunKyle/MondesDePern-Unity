using UnityEngine;
using System.Collections;

public class DragonBlue : MonoBehaviour {
	// Dragon infos
	private Transform thisTransform;
	private Rigidbody thisRigidbody;
	public Transform spriteParent;
	public OTAnimatingSprite dragonSprite;
	private int Life;
		
	// Player infos
	public Transform spritePlayer;
	
	void Awake()
	{
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
	
	// Use this for initialization
	void Start () {
		Life = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
