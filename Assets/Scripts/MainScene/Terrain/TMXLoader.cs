using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;
using TMXObjects;

public class TMXLoader : MonoBehaviour {
	
	public TextAsset mapXML;
	
	/// <summary>
	/// The tile offset between orthello and Tiled Map Editor.
	/// </summary>
	public int tileOffset = 0;
	
	private TMap map;
	
	public TMap Map { get; set; }
	
	// Use this for initialization
	void Start () {
		//loadLevel("Assets/Resources/testLvl.tmx");	
		//mapXML = (TextAsset) Resources.Load("testLvl_2", typeof(TextAsset));
		loadLevel(mapXML.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Loads the level.
	/// </summary>
	/// <param name='filename'>
	/// Filename of the tmx file.
	/// </param>
	void loadLevel(string filename){
		
		map = _TMXLoader.LoadMap(filename);
		//offsets to put sprites at the right position
		//In Orthello (0,0) is the center of the view,
		//so we need to make a translation to the top left corner
		//of the view.
		int xoffset = (map.Width*map.TileWidth)/2 - map.TileWidth/2;
		int yoffset = (map.Height*map.TileHeight)/2 - map.TileHeight/2;
		
		OT.RuntimeCreationMode();
				
		foreach (TLayerBase layer in map.LayerBaseList){
	        for (int i = 0; i < ((TLayer)layer).Data.Length; ++i){
				string prototype = getOrthelloPrototype(((TLayer)layer).Data[i]);
				bool hasToTiled = prototype != "None";
								
				if (hasToTiled){
					OT.CreateSpriteAt(prototype, new Vector2(-xoffset + (i%map.Width)*map.TileWidth, yoffset - (i/map.Width)*map.TileHeight));
					hasToTiled = false;
				}
			}
		}
	}
	
	/// <summary>
	/// Gets the index of the orthello frame from tile ID.
	/// </summary>
	/// <returns>
	/// The orthello frame index.
	/// </returns>
	/// <param name='tileId'>
	/// Tile identifier from Tiled Map Editor.
	/// </param>
	string getOrthelloPrototype(int tileId){
		if (tileId == 0)
			return "None";
		
		foreach (TTileSet tileSet in map.TileSetList){
			//tileId-tileSet.FirstGID because map data are stored with 0 for empty
			//and tileset's tiles start from 0 too !
			//therefore everything is shifted by tileset first GID.
			int newTileId = tileId-tileSet.FirstGID;
			if (tileSet.TilePropertyList.ContainsKey(newTileId)){
				//we are in the right tileset
				if (tileSet.TilePropertyList[newTileId].ContainsKey("prototype")){
					//got custom index property
					return tileSet.TilePropertyList[newTileId]["prototype"];
				}
			}
		}
		
		//if no custom index property has set,
		//then use the tileOffset
		return "None";
	}
}
