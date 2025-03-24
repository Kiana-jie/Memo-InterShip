using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sellable : Item
{
    public int SellPrice { get; set; }

    public Sellable(int sellPrice, int iD, string name, ItemType itemType, string description, int capicity, string sprite) : base(iD, name, itemType, description, capicity, sprite)
    {
        SellPrice = sellPrice;
    }

    public Sellable() { }
}
