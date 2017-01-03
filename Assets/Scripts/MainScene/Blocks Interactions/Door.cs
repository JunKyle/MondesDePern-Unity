using UnityEngine;

public class Door : MonoBehaviour
{
	private OTAnimatingSprite sprite;
	private BoxCollider doorCollider;
	private Vector3 doorColliderPos;
	private bool isOpened;
	
	void Start()
	{
		sprite = GetComponent<OTAnimatingSprite>();
		sprite.onInput = onInput;
		doorCollider = GetComponentInChildren<BoxCollider>();
		doorColliderPos = doorCollider.transform.position;
		isOpened = false;
	}
	
	protected virtual void onInput (OTObject owner)
	{
	    // right mouse button clicked
	    if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
	    {
			float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
			if (distance < 150)
			{
				if (isOpened)
				{
					sprite.PlayOnceBackward("Open");
					doorCollider.transform.position = doorColliderPos;
					isOpened = false;
				}
				else
				{
					sprite.PlayOnce("Open");
					doorCollider.transform.position = new Vector3(-4000, -4000, -200);
					isOpened = true;
				}
			}
	    }
	}
}