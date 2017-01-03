using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	public GUISkin inventorySkin;
	
	private float buttonWitdh = 40;
	private float buttonHeight = 40;
	
	/*********************************************/
	/* Inventory window variables */
	/*********************************************/
	public int inventoryRows = 5;
	public int inventoryCols = 4;
	public float inventoryX = 910;
	public float inventoryY = 384;
	public float inventoryWidth = 170;
	public float inventoryHeight = 225;
	
	private const int INVENTORY_WINDOW_ID = 0;
	private static bool displayInventory;
	private Rect inventoryRect;
	
	/*********************************************/
	/* Loot window variables */
	/*********************************************/
	public int lootRows = 2;
	public int lootCols = 4;
	public float lootX = 910;
	public float lootY = 384;
	public float lootWidth = 170;
	public float lootHeight = 120;
	
	private const int LOOT_WINDOW_ID = 1;
	private static bool displayLoot;
	private Rect lootRect;
	
	
	// item selected
	public static Item currentBlock = null;
	public static Dictionary<string, List<Item>> lootTable;
	
	
	void Start()
	{
		inventoryX = Screen.width - inventoryWidth - 20;
		inventoryY = Screen.height - inventoryHeight - 20;
		inventoryRect = new Rect(inventoryX, inventoryY, inventoryWidth, inventoryHeight);
		
		lootY = Screen.height - lootHeight - 20;
		lootRect = new Rect(lootX, lootY, lootWidth, lootHeight);
		
		displayInventory = true;
		displayLoot = false;
		
		lootTable = new Dictionary<string, List<Item>>();
	}
	
	void OnGUI()
	{
		GUI.skin = inventorySkin;
		
		if (displayInventory)
		{
			inventoryRect = GUI.Window(INVENTORY_WINDOW_ID, inventoryRect, InventoryWindow, "Inventaire", GUI.skin.FindStyle("inventory background"));
		}
		
		if (displayLoot)
		{
			lootRect = GUI.Window(LOOT_WINDOW_ID, lootRect, LootWindow, "Coffre", GUI.skin.FindStyle("inventory background"));
		}
		
		if (currentBlock != null)
		{
			GUI.Label(new Rect(10, Screen.height - 20, 200, 20), "Vous tenez actuellement : " + currentBlock.TypeID + ".");
		}
		else
		{
			GUI.Label(new Rect(10, Screen.height - 20, 200, 20), "Rien dans les mains !");
		}
	}
	
	void Update()
	{	
		if(Input.GetKeyUp(KeyCode.I))
		{
			ToggleInventoryWindow();
		}
	}
	
	public void InventoryWindow(int id)
	{
		List<GUIContent> itemsToShow = new List<GUIContent>();
		
		foreach(string s in Player.inventory.Keys)
		{
			GUIContent content = new GUIContent(Player.inventory[s].Count.ToString(), Player.inventory[s][0].InventoryIcon);
			itemsToShow.Add(content);
		}

		int y = 0;
		int x = 0;
		
		foreach (GUIContent c in itemsToShow)
		{
			if (x >= inventoryCols)
			{
				x = 0;
				y++;
			}
			
			if (GUI.Button(new Rect(5 + x * buttonWitdh, 20 + y * buttonHeight, buttonWitdh, buttonHeight), c, GUI.skin.FindStyle("slot")))
			{
				setCurrentItem(c);
			}
			
			x++;
		}
		
		// sets the window draggable
		GUI.DragWindow();
	}
	
	public void LootWindow(int id)
	{
		List<GUIContent> itemsToShow = new List<GUIContent>();
		
		foreach(string s in lootTable.Keys)
		{
			GUIContent content = new GUIContent(lootTable[s].Count.ToString(), lootTable[s][0].InventoryIcon);
			itemsToShow.Add(content);
		}

		int y = 0;
		int x = 0;
		
		foreach (GUIContent c in itemsToShow)
		{
			if (x >= lootCols)
			{
				x = 0;
				y++;
			}
			
			if (GUI.Button(new Rect(5 + x * buttonWitdh, 20 + y * buttonHeight, buttonWitdh, buttonHeight), c, GUI.skin.FindStyle("slot")))
			{
				getLoot(c);
			}
			
			x++;
		}
	}
	
	public static void ToggleInventoryWindow()
	{
		// open/close inventory window
		displayInventory = !displayInventory;
		currentBlock = null;
	}
	
	public static void ToggleLootWindow()
	{
		// open/close loot window
		displayLoot = !displayLoot;
	}
	
	public static void CloseLootWindow()
	{
		displayLoot = false;
	}
	
	private void setCurrentItem(GUIContent c)
	{
		foreach(string s in Player.inventory.Keys)
		{
			if (c.image.Equals(Player.inventory[s][0].InventoryIcon))
			{
				currentBlock = Player.inventory[s][0];
				return;
			}
		}
	}
	
	private void getLoot(GUIContent c)
	{
		foreach(string s in lootTable.Keys)
		{
			if (c.image.Equals(lootTable[s][0].InventoryIcon))
			{
				Player.addItem(lootTable[s][0]);
				Chest.itemToRemove = lootTable[s][0];
				return;
			}
		}
	}
}
