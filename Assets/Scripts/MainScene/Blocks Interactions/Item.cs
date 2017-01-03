using UnityEngine;
using System.Collections;

public class Item {
	
	private string typeID;
	private Texture inventoryIcon;
	private bool restorable;
	
	public bool Restorable {get; set;}
	public Texture InventoryIcon {get; set;}
	public string TypeID {get; set;}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Item"/> class.
	/// </summary>
	/// <param name='id'>
	/// Identifier.
	/// </param>
	/// <param name='icon'>
	/// Inventory icon texture.
	/// </param>
	/// <param name='rest'>
	/// Restorable property.
	/// </param>
	public Item(string id, Texture icon, bool rest){
		TypeID = id;
		InventoryIcon = icon;
		Restorable = rest;
	}
}
