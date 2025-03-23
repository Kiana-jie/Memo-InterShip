using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuyableType
{
    Consumable,//����Ʒ
    Equipment,//װ��
}
public class Buyable : Item
{
    public int BuyPrice { get; set; }
    public BuyableType buyableType { get; set; }
    public Buyable(int buyPrice, BuyableType buyableType)
    {
        BuyPrice = buyPrice;
        this.buyableType = buyableType;
    }
    public Buyable() { }
}
