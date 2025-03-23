using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{
    public TileBase tile;
    public int health;
    

    public TileData(TileBase tile, int health)
    {
        this.tile = tile;
        this.health = health;
    }

    
}
