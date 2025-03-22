using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("基本属性")]
    public int maxHealth;
    public int maxOxygen;
    public int speed;
    public int digForce;
    public int defenceForce;
    public float flyForce;
    //public int maxSpeed;
    [SerializeField]
    private int curhealth;
    [SerializeField]
    private int curOxygen;

    [Header("基本状态")]
    public bool isGrounded;
    public bool isLeftwalled;
    public bool isRightwalled;

    //动作执行条件
    public bool canDig = false;
    // Start is called before the first frame update
    void Start()
    {
        flyForce = speed * 0.2f;
        curhealth = maxHealth; curOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
    }

    public void CheckCondition()
    {
        if(curhealth <= 0)
        {
            Die();
        }
        if(curOxygen <= 0)
        {
            TakeDamage(20);
        }
        if(isGrounded)
        {
            canDig = true;
        }
        else canDig = false;
    }

    public void Die()
    {

    }

    public void TakeDamage(int damage)
    {
        curhealth -= damage;
    }
}
