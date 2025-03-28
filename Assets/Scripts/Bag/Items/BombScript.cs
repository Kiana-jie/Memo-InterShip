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
        Debug.Log(Damage);
        StartCoroutine(ExplodeAfterDelay(3f));
    }

    public IEnumerator ExplodeAfterDelay(float t)
    {
        yield return new WaitForSeconds(t);
        Explode();
    }

    private void Explode()
    {
        AudioManager.Instance.Play("bomb", gameObject);
        //Debug.Log("start");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius,LayerMask.GetMask("Monster"));
        Debug.Log(colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            // 如果碰到怪物，则造成伤害
            Monster monster = collider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(Damage);
                Debug.Log($"对怪物 {monster.name} 造成 {Damage} 点伤害");
            }
            else Debug.Log("没有找到monster");
        }
        Destroy(gameObject);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
