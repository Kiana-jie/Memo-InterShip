using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{
    public TileBase tile;
    public int health;
    public bool isOre;
    public int itemID;
    

    public TileData(TileBase tile, int health, bool isOre,int itemID)
    {
        this.tile = tile;
        this.health = health;
        this.isOre = isOre;
        this.itemID = itemID;
    }

    
}
