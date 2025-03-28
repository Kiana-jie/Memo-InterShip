using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMove : Consumable
{
    public Vector2 startPoint { get; set; } 
    public Vector3 endPoint {  get; set; }
    public FastMove(Vector2 startPoint,Vector2 endPoint, ConsumableType consumableType, int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description, int capicity, string sprite) : base(consumableType, buyPrice, buyableType, iD, name, itemType, description, capicity, sprite)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }

    public override void Consume()
    {
        Transform transform = GameObject.Find("Player").GetComponent<Transform>();
        startPoint = transform.position;
        transform.position = endPoint;
    }
}
