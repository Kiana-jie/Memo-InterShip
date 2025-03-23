using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverWater : Consumable
{
    public int HP {  get;  set; }//ΡͺΑΏ
    public int OP {  get; set; }//ΡυΖψ

    public RecoverWater(int HP,int OP, ConsumableType consumableType, int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description, string sprite):base(consumableType, buyPrice, buyableType, iD, name, itemType, description, sprite)
    {
        this.HP = HP;
        this.OP = OP;
    }

}
