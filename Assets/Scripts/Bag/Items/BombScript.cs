using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public int Damage { get; set; }
    public float ExplosionRadius = 5f; // 爆炸范围
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplodeAfterDelay(3f));
    }

    public IEnumerator ExplodeAfterDelay(float t)
    {
        yield return new WaitForSeconds(t);
        Explode();
    }

    private void Explode()
    {
       /* // 取得爆炸范围内的所有怪物（假设怪物有 `Monster` 标签）
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Monster"))
            {
                // 如果碰到怪物，则造成伤害
                Monster monster = collider.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.TakeDamage(Damage);
                    Debug.Log($"对怪物 {monster.name} 造成 {Damage} 点伤害");
                }
            }
        }*/
        Destroy(gameObject);

    }
}
