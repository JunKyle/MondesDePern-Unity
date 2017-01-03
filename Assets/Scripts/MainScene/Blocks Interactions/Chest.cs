using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {
	
	protected OTSprite sprite;
	
	// Loot table
	private Dictionary<string, List<Item>> chestContent = new Dictionary<string, List<Item>>();
	public Dictionary<string, List<Item>> ChestContent {
		get {
			return this.chestContent;
		}
		set {
			chestContent = value;
		}
	}
	
	public static Item itemToRemove = null;
	
	public void addItem(Item block)
	{
		if (ChestContent.ContainsKey(block.TypeID))
			ChestContent[block.TypeID].Add(block);
		else
		{
			List<Item> list = new List<Item>();
			list.Add(block);
			ChestContent.Add(block.TypeID, list);
		}
	}
	
	public void removeChestItem(Item block)
	{
		if (ChestContent.ContainsKey(block.TypeID))
		{
			if (ChestContent[block.TypeID].Count > 1)
			{
				ChestContent[block.TypeID].Remove(block);
			}
			else
			{
				ChestContent[block.TypeID].Remove(block);
				ChestContent.Remove(block.TypeID);
			}
		}
		
		itemToRemove = null;
	}
	
	// Use this for initialization
	void Start () {	
		sprite = GetComponent<OTSprite>();
		sprite.onInput = onInput;
		
		// clearing content for reloading
		ChestContent.Clear();
		
		// adding loot to chest
		AddChestLoot("books");
		AddChestLoot("shelf");
		AddChestLoot("vials");
		AddChestLoot("torch");
		AddChestLoot("table");
		AddChestLoot("chair");
	}
	
	void AddChestLoot(string name, int nb = 1)
	{
		for (int i = 0; i < nb; ++i)
		{
			DestructibleBlock item = (DestructibleBlock) OTObjectType.lookup[name].GetComponent<DestructibleBlock>();
			addItem(new Item(item.typeID, item.inventoryIcon, item.restorable));
		}
	}
	
	// Input handler
	protected virtual void onInput (OTObject owner)
	{
	    // right mouse button clicked
	    if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
	    {
			float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
			if (distance < 150)
			{
				Inventory.lootTable = ChestContent;
				Inventory.ToggleLootWindow();
			}
	    }
	}
	
	// Update is called once per frame
	void Update () {
		// if the player goes too far, the chest closes itself
		float distance = Vector3.Distance(MainSceneGlobal.character.transform.position, transform.position);
		if (distance > 150)
		{
			Inventory.CloseLootWindow();
		}
		
		// remove an item if it is picked by the player
		if (itemToRemove != null)
		{
			removeChestItem(itemToRemove);
		}
	}
}
