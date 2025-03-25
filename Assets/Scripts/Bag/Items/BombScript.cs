using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public int Damage { get; set; }
    public float ExplosionRadius = 5f; // ��ը��Χ
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
       /* // ȡ�ñ�ը��Χ�ڵ����й����������� `Monster` ��ǩ��
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Monster"))
            {
                // ����������������˺�
                Monster monster = collider.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.TakeDamage(Damage);
                    Debug.Log($"�Թ��� {monster.name} ��� {Damage} ���˺�");
                }
            }
        }*/
        Destroy(gameObject);

    }
}
