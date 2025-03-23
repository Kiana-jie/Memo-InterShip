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

    public Consumable(ConsumableType consumableType)
    {
        this.consumableType = consumableType;
    }
    public Consumable() { }
}
