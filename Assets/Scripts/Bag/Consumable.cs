using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    RecoverWater,
    Bomb,
    FastMove
}
public class Consumable : Buyable
{
    public ConsumableType consumableType;

    public Consumable(ConsumableType consumableType, int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description,int capicity, string sprite) : base(buyPrice, buyableType,iD, name, itemType, description,capicity, sprite)
    {
        this.consumableType = consumableType;
    }
    public Consumable() { }
}
