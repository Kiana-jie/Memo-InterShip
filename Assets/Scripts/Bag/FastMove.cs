using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMove : Consumable
{
    public Vector3Int startPoint { get; set; } 
    public Vector3Int endPoint {  get; set; }
    public FastMove(Vector3Int startPoint,Vector3Int endPoint) 
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }

    public override void Consume()
    {
        
    }
}
