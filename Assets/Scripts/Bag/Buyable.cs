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
    public Buyable(int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description,int capicity,  string sprite):base(iD, name, itemType, description, capicity, sprite)
    {
        BuyPrice = buyPrice;
        this.buyableType = buyableType;
    }
    public Buyable() { }
}
