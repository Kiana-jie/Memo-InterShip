using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Consumable
{
   public int Damage {  get;  set; }
    public Bomb(int damage, ConsumableType consumableType, int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description, int capicity, string sprite) : base(consumableType, buyPrice, buyableType, iD, name, itemType, description, capicity, sprite)
    {
        Damage = damage;
    }
}
