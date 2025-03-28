using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("基本属性")]
    public int maxHealth;
    public float maxOxygen;
    public int speed;
    public int digForce;
    public float digCoolDown;
    public int defenceForce;
    public float flyForce;
    public float invulnerableDuration = 1f;
    
    public bool invulnerable;
    public int money = 0;
    //public int maxSpeed;
    [SerializeField]
    private int curHealth;
    [SerializeField]
    private float curOxygen;
    
    public float lastDigTime = 0;
    [Header("基本位置状态")]
    public bool isGrounded;
    public bool isLeftwalled;
    public bool isRightwalled;

    [Header("人物物理状态")]
    public bool isFlying;

    public Image healthBar;
    public Image OxygenBar;
    private Animator anim;
    private bool isOxygenDecreasing = false;
    private bool isHurting = false;
    private Rigidbody2D rb;
    //动作执行条件
    public bool canInput = true;
    public bool canDig = false;
    public static PlayerStatus Instance { get; private set; }

    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        flyForce = speed * 0.1f;
        curHealth = maxHealth; curOxygen = maxOxygen;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
    }

    public void CheckCondition()
    {

        if (curHealth <= 0)
        {
            Die();
        }
        if(transform.position.y >= -5) 
        {
            curOxygen = maxOxygen;
        }
        if(transform.position.y < -5 && !isOxygenDecreasing && curOxygen > 0)
        {
            StartCoroutine(DecreaseOxygen());
            
        }
        if (curOxygen <= 0 && !isHurting && transform.position.y < -5)
        {
            StartCoroutine(takeOxyHurt());//此处应无僵直
            
        }
        if (isGrounded  || isFlying)
        {
            canDig = true;
            rb.gravityScale = 1f;
        }
        
        else 
        {
            canDig = false;
            rb.gravityScale = 2f;
        }
        
        CheckFallDamage();
    }

    public void CheckFallDamage()
    {
        if (isGrounded && rb.velocity.y <= -15) // 速度低于阈值且落地
        {
            TakeDamage(20);
            
        }
    }
    private IEnumerator DecreaseOxygen()
    {
        isOxygenDecreasing = true;
        yield return new WaitForSeconds(1f);
        curOxygen -= 0.5f;
        UpdateBarUI();
        isOxygenDecreasing = false;

    }

    private IEnumerator takeOxyHurt()
    {
        isHurting = true;
        yield return new WaitForSeconds(1f);
        curHealth -= 5;
        UpdateBarUI();
        isHurting = false;
    }
    public void Die()
    {
        //死亡动画
        anim.SetBool("isDie", true);
        canInput = false;
        StartCoroutine(DieDown());
        


    }
    

    


    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            curHealth -= damage;
            //受击动画
            invulnerable = true;
            UpdateBarUI();
            if (curHealth < 0) { Die(); return; }
            anim.SetBool("isAttacked", true);
            Debug.Log("attacked!");
            StartCoroutine(InputCoolDown(1f));
           
            StartCoroutine(InvulnerabilityTimer());

        }
        
        
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("当前金币：" + money);
    }

    public void RecoverHealth(int amount)
    {
        curHealth += amount;
        if (curHealth > maxHealth) curHealth = maxHealth;
        Debug.Log($"恢复 {amount} 生命值，当前血量：{curHealth}");
        //血量UI
        UpdateBarUI();
    }

    public void RecoverOxygen(int amount)
    {
        curOxygen += amount;
        if(curOxygen > maxOxygen) curOxygen = maxOxygen;
        Debug.Log($"恢复 {amount} 氧气，当前氧气量：{curOxygen}");
        //氧气UI
        UpdateBarUI();
    }

    public void UpdateBarUI()
    {
        healthBar.fillAmount = (float)curHealth / maxHealth;
        OxygenBar.fillAmount = (float)curOxygen / maxOxygen;

    }
    private IEnumerator InputCoolDown(float t)
    {
        canInput = false;
        yield return new WaitForSeconds(t);
        canInput = true;
        anim.SetBool("isAttacked", false);
    }
    private IEnumerator InvulnerabilityTimer()
    {
        
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }
    private IEnumerator DieDown()
    {
        yield return new WaitForSeconds(2f);
        //唤醒死亡后面板UI
        Destroy(gameObject);
    }

}
