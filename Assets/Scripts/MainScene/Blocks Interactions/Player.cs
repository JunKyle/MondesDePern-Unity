using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour 
{
	// Life
	public static int life = 10;
	
	// Strength of pickaxe
	public static int pickaxeStrenght = 1;
	
	public static float blockResolution = 64f;
	
	
	// Inventory
	public static Dictionary<string, List<Item>> inventory = new Dictionary<string, List<Item>>();
	
	public static void addItem(Item block)
	{
		if (inventory.ContainsKey(block.TypeID))
			inventory[block.TypeID].Add(block);
		else
		{
			List<Item> list = new List<Item>();
			list.Add(block);
			inventory.Add(block.TypeID, list);
		}
	}
	
	public static void removeInventoryItem(Item block)
	{
		if (inventory.ContainsKey(block.TypeID))
		{
			if (inventory[block.TypeID].Count > 1)
			{
				Inventory.currentBlock = inventory[block.TypeID][1];
				inventory[block.TypeID].Remove(block);
				
			}
			else
			{
				Inventory.currentBlock = null;
				inventory[block.TypeID].Remove(block);
				inventory.Remove(block.TypeID);
			}
		}
	}
	
	void Start ()
	{
		// adding items to inventory
		AddInventoryLoot("bed");
		AddInventoryLoot("craftingtable");
		AddInventoryLoot("door");
	}
	
	void AddInventoryLoot(string name, int nb = 1)
	{
		for (int i = 0; i < nb; ++i)
		{
			DestructibleBlock item = (DestructibleBlock) OTObjectType.lookup[name].GetComponent<DestructibleBlock>();
			addItem(new Item(item.typeID, item.inventoryIcon, item.restorable));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if (Input.GetMouseButtonUp(1) && GUIUtility.hotControl == 0)
	    {
			if (Inventory.currentBlock != null)
			{
				if (Inventory.currentBlock.TypeID == "mirror")
				{
					Player.inventory.Clear();
					Application.LoadLevel("MainScene");
				}
				else
				{
					checkDroppingBlock();
				}
			}
		}
	}
	
	/**********************************************/
	void checkDroppingBlock()
	{
		// if selected block is dropable
		if (Inventory.currentBlock.Restorable)
		{
			// if there is nothing where we want to drop it
			if (OT.ObjectsUnderPoint().Length == 0)
			{
				// check if there is a block that can serve as a support
				if (!tryToDropBlock(Vector3.right))
					if (!tryToDropBlock(Vector3.left))
						if (!tryToDropBlock(Vector3.up))
							tryToDropBlock(Vector3.down);						
			}
		}
	}
	
	/**********************************************/
	private bool tryToDropBlock(Vector3 direction)
	{
		// check if there is a block to drop on on chosen direction
		RaycastHit hit;
		Vector2 pos = OT.view.mouseWorldPosition;
		Vector3 newPos = new Vector3(pos.x, pos.y, transform.position.z);
		if (Physics.Raycast(newPos, direction, out hit, blockResolution))
		{
			// if a block is found
			GameObject obj = hit.collider.gameObject;
			OTSprite sprite = OT.Sprite(obj.name);
			
			// cannot drop a block on the player nor the dragon
			if (!obj.name.Contains("Dwarf") && !obj.name.Contains("Dragon"))
			{
				// check if distance is correct between drop location and player
				float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, obj.transform.position);
				if (distance < 150)
				{
					float finalX = 0;
					float finalY = 0;
					// take the size of the dropable into account
					OTSprite blockToDrop = OT.CreateSprite(Inventory.currentBlock.TypeID);
					int nbX = (int) blockToDrop.size.x / (int) blockResolution;
					int nbY = (int) blockToDrop.size.y / (int) blockResolution;
					OT.DestroyObject(blockToDrop);
					
					if (direction == Vector3.down)
					{
						// pos to drop the block
						if (nbX > 1)
						{
							finalX = sprite.position.x + nbX * (blockResolution / 4f);
						}
						else
						{
							finalX = sprite.position.x;
						}
						if (nbY > 1)
						{
							finalY = sprite.position.y + nbY * (blockResolution/1.4f);
						}
						else
						{
							finalY = sprite.position.y + blockResolution;
						}
					}
					else if (direction == Vector3.up)
					{						
						// pos to drop the block
						if (nbX > 1)
						{
							finalX = sprite.position.x + nbX * (blockResolution / 4f);
						}
						else
						{
							finalX = sprite.position.x;
						}
						if (nbY > 1)
						{
							finalY = sprite.position.y - nbY * (blockResolution/1.3f);
						}
						else
						{
							finalY = sprite.position.y - blockResolution;
						}
					}
					else if (direction == Vector3.left)
					{
						// pos to drop the block
						if (nbX > 1)
						{
							finalX = sprite.position.x + nbX * (blockResolution / 1.4f);
						}
						else
						{
							finalX = sprite.position.x + blockResolution;
						}
						if (nbY > 1)
						{
							finalY = sprite.position.y + nbY * (blockResolution/4f);
						}
						else
						{
							finalY = sprite.position.y;
						}
					}
					else
					{
						// pos to drop the block
						if (nbX > 1)
						{
							finalX = sprite.position.x - nbX * (blockResolution / 1.4f);
						}
						else
						{
							finalX = sprite.position.x - blockResolution;
						}
						if (nbY > 1)
						{
							finalY = sprite.position.y + nbY * (blockResolution/4f);
						}
						else
						{
							finalY = sprite.position.y;
						}
					}

					OT.CreateSpriteAt(Inventory.currentBlock.TypeID, new Vector2 (finalX, finalY));
					removeInventoryItem(Inventory.currentBlock);
					return true;
				}
			}
		}
		return false;
	}
}
