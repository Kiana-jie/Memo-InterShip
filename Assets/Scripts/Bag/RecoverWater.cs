using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverWater : Consumable
{
    public int HP {  get;  set; }//Ѫ��
    public int OP {  get; set; }//����

    public RecoverWater(int HP,int OP, ConsumableType consumableType, int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description,int capicity, string sprite):base(consumableType, buyPrice, buyableType, iD, name, itemType, description,capicity, sprite)
    {
        this.HP = HP;
        this.OP = OP;
    }
    public override void Consume()
    {
        PlayerStatus.Instance.RecoverHealth(HP);
    }
}
