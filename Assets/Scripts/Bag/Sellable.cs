using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sellable : Item
{
    public int SellPrice { get; set; }

    public Sellable(int sellPrice)
    {
        SellPrice = sellPrice;
    }

    public Sellable() { }
}
