using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private PlayerStatus status;
    private Rigidbody2D rb;
    private PhysicCheck physicCheck;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        physicCheck = GetComponent<PhysicCheck>();

        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleDig();
    }


    private void FixedUpdate()
    {
        HandleMovement();
        HandleFly();
        //SpeedLimit();
    }
    public void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.localScale = new Vector3(-1,1,1);
            rb.velocity = new Vector2(-status.speed , rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(status.speed , rb.velocity.y);//���ܳ�bug�ĵط�
            
        }

    }
    public void HandleFly()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
              rb.AddForce(Vector3.up * status.flyForce, ForceMode2D.Impulse);   
        }
    }
    public void HandleDig()
    {
        if (status.canDig == false) { return; }
        else
        {
            Vector3Int tilePosition = physicCheck.GetTilePosition();
            if (Input.GetKey(KeyCode.DownArrow)) { DigPostion(tilePosition+Vector3Int.down); }//������
            else if (status.isLeftwalled == true && Input.GetKey(KeyCode.LeftArrow)) { DigPostion(tilePosition+Vector3Int.left); }//������
            else if(status.isRightwalled == true && Input.GetKey(KeyCode.RightArrow)) { DigPostion(tilePosition+Vector3Int.right); }//������            
        }
    }

    public void DigPostion(Vector3Int pos)
    {
        if (physicCheck.tilemap.HasTile(pos))
        {
            physicCheck.tilemap.SetTile(pos, null); // �Ƴ��ؿ�
            physicCheck.UpdateTilemapCollider(); // ���������ײ��
        }
    }




    /*public void SpeedLimit()
    {
        if(rb.velocity.y < -status.maxSpeed )
        {
            rb.velocity = new Vector2(rb.velocity.x, -status.maxSpeed);
        }

        else if (rb.velocity.y > status.maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, status.maxSpeed);
        }
    }*/
}
