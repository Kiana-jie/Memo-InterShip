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
    public int money = 0;
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
    private float lastOxygenTime;

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
        if(transform.position.y < -5)
        {
            StartCoroutine(OxCoolDown());
            
        }
        if (curOxygen <= 0)
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
    private IEnumerator OxCoolDown()
    {
        yield return new WaitForSeconds(1f);
        curOxygen -= 5;
        
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
            invulnerable = true;
            UpdateBarUI();
            anim.SetBool("isAttacked", true);
            Debug.Log("attacked!");
            StartCoroutine(InputCoolDown(1f));
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
        yield return new WaitForSeconds(t);
        canInput = true;
        anim.SetBool("isAttacked", false);
    }
    private IEnumerator InvulnerabilityTimer()
    {
        
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

}
