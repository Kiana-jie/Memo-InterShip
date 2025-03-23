using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Consumable
{
   public int Damage {  get;  set; }
   public Bomb(int damage)
    {
        Damage = damage;
    }
}
