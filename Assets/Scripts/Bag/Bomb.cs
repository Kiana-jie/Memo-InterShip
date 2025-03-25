using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Consumable
{
   public int Damage {  get;  set; }
    public Bomb(int damage, ConsumableType consumableType, int buyPrice, BuyableType buyableType, int iD, string name, ItemType itemType, string description, int capicity, string sprite) : base(consumableType, buyPrice, buyableType, iD, name, itemType, description, capicity, sprite)
    {
        Damage = damage;
    }

    public override void Consume()
    {
        GameObject bombPrefab = Resources.Load<GameObject>("Prefabs/Items/Bomb");
        if (bombPrefab != null)
        {
            GameObject bomb = GameObject.Instantiate(bombPrefab, PlayerStatus.Instance.transform.position, Quaternion.identity);
            // …Ë÷√’®µØµƒ’®µØΩ≈±æ
            BombScript bombScript = bomb.GetComponent<BombScript>();
            if (bombScript != null)
            {
                bombScript.Damage = this.Damage;
                
            }
        }
        else
        {
            Debug.LogError("’®µØ‘§÷∆ÃÂŒ¥’“µΩ£°");
        }
    }
}
