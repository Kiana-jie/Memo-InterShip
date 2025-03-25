using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;
    public float attackRange;
    public float invulnerableDuration = 10f;
    private float invulnerableCounter;
    public bool invulnerable;
    private int faceDir = 1; // 1Ϊ�ң�-1Ϊ��
    private Rigidbody2D rb;
    public LayerMask layerMask; // ���ڼ��ǽ��
    public float groundCheckRadius = 0.2f; // ��ⷶΧ�İ뾶
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        CheckDir(); // ����Ƿ���Ҫ�ı䷽��
        rb.velocity = new Vector2(speed * faceDir * Time.deltaTime, rb.velocity.y);
    }

    // �������Ƿ�Ӧ�øı䷽��
    public void CheckDir()
    {
       // ����λ���Ƿ���ǽ��
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * faceDir, 0.5f, layerMask);
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position + Vector3.right* faceDir*0.1f,Vector2.down,0.5f,layerMask);

        

        if (wallHit.collider != null || groundHit.collider == null) // ���ǰ����ǽ�����û�е���
        {
            // �ı䷽��
            faceDir *= -1;
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
        }
    }

   public void Attack()
   {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player"));
        collider.GetComponent<PlayerStatus>().TakeDamage(damage);
        Debug.Log($"����ܵ���{damage}���˺�");
   }

    // ʹ��������޵�״̬
    public void TakeDamage(int damageAmount)
    {
        if (!invulnerable)
        {
            health -= damageAmount;
            if(health <= 0)//����
            {
                Destroy(gameObject);
            }
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
            StartCoroutine(InvulnerabilityTimer());
        }
    }

    // �����޵�״̬�ļ�ʱ��
    private IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
