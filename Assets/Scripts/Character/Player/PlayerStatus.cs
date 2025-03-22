using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("»ù±¾ÊôÐÔ")]
    public int maxHealth;
    public int maxOxygen;
    public int speed;
    public int digForce;
    public int defenceForce;
    public float flyForce;
    public int maxSpeed;
    [SerializeField]
    private int curhealth;
    [SerializeField]
    private int curOxygen;

    public bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        flyForce = speed * 0.2f;
        curhealth = maxHealth; curOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
