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
    private int faceDir = 1; // 1为右，-1为左
    private Rigidbody2D rb;
    public LayerMask layerMask; // 用于检测墙壁
    public float groundCheckRadius = 0.2f; // 检测范围的半径
    

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
        CheckDir(); // 检查是否需要改变方向
        rb.velocity = new Vector2(speed * faceDir * Time.deltaTime, rb.velocity.y);
    }

    // 检查怪物是否应该改变方向
    public void CheckDir()
    {
       // 检查该位置是否有墙壁
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * faceDir, 0.5f, layerMask);
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position + Vector3.right* faceDir*0.1f,Vector2.down,0.5f,layerMask);

        

        if (wallHit.collider != null || groundHit.collider == null) // 如果前方有墙或脚下没有地面
        {
            // 改变方向
            faceDir *= -1;
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
        }
    }

   public void Attack()
   {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player"));
        collider.GetComponent<PlayerStatus>().TakeDamage(damage);
        Debug.Log($"玩家受到了{damage}的伤害");
   }

    // 使怪物进入无敌状态
    public void TakeDamage(int damageAmount)
    {
        if (!invulnerable)
        {
            health -= damageAmount;
            if(health <= 0)//死亡
            {
                Destroy(gameObject);
            }
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
            StartCoroutine(InvulnerabilityTimer());
        }
    }

    // 控制无敌状态的计时器
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
