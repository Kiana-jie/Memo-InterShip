using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("基本属性")]
    public int maxHealth;
    public int maxOxygen;
    public int speed;
    public int digForce;
    public float digCoolDown;
    public int defenceForce;
    public float flyForce;
    public float invulnerableDuration = 1f;
    
    public bool invulnerable;
    public int money = 1000;
    //public int maxSpeed;
    [SerializeField]
    private int curHealth;
    [SerializeField]
    private int curOxygen;
    
    public float lastDigTime = 0;
    [Header("基本位置状态")]
    public bool isGrounded;
    public bool isLeftwalled;
    public bool isRightwalled;

    public Image healthBar;
    public Image OxygenBar;
    private Animator anim;

    //动作执行条件
    public bool canInput = true;
    public bool canDig = false;
    public static PlayerStatus Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        flyForce = speed * 0.2f;
        curHealth = maxHealth; curOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
    }

    public void CheckCondition()
    {

        if(curHealth <= 0)
        {
            Die();
        }
        if(curOxygen <= 0)
        {
            TakeDamage(20);
            UpdateBarUI();
        }
        if (isGrounded)
        {
            canDig = true;
        }
        else
        {
            canDig = false;
        }
    }
    
    public void Die()
    {
        //死亡动画
        Destroy(gameObject);

    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            curHealth -= damage;
            //受击动画
            //anim.SetBool("isAttacked", true);
            invulnerable = true;
            StartCoroutine(InputCoolDown(1f));
            UpdateBarUI();
            if (curHealth < 0) { Die(); }
           
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
        yield return new WaitForSecondsRealtime(t);
        anim.SetBool("isAttacked", false);
        canInput = true;
    }
    private IEnumerator InvulnerabilityTimer()
    {
        
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

}
