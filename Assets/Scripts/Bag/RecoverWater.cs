using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverWater : Consumable
{
    public int HP {  get;  set; }//ΡͺΑΏ
    public int OP {  get; set; }//ΡυΖψ

    public RecoverWater(int HP,int OP) 
    {
        this.HP = HP;
        this.OP = OP;
    }
}
