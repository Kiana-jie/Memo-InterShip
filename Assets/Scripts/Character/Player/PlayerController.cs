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
    private TilesManagement tesManager;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        physicCheck = GetComponent<PhysicCheck>();
        tesManager = GameObject.Find("Layer-grounds").GetComponent<TilesManagement>();

        
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
            rb.velocity = new Vector2(status.speed , rb.velocity.y);//可能出bug的地方
            
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
        //播放dig动画
        if (status.canDig == false) { return; }
        if(Time.time - status.lastDigTime < status.digCoolDown) { return; }
        else
        {
            
            Vector3Int tilePosition = physicCheck.GetTilePosition();
            if (Input.GetKey(KeyCode.DownArrow)) { DigTile(tilePosition+Vector3Int.down); }//向下挖
            else if (status.isLeftwalled == true && Input.GetKey(KeyCode.LeftArrow)) { DigTile(tilePosition+Vector3Int.left); }//向左挖
            else if(status.isRightwalled == true && Input.GetKey(KeyCode.RightArrow)) { DigTile(tilePosition+Vector3Int.right); }//向右挖            
        }
    }

    public void DigTile(Vector3Int pos)
    {
        if (physicCheck.tilemap.HasTile(pos))
        {
            //优化：只需考虑player周围的地块？
            
            tesManager.tiles[pos].health -= status.digForce;
            tesManager.PlayDamageAnimation(pos);
            if (tesManager.tiles[pos].health <= 0)
            {
                physicCheck.tilemap.SetTile(pos, null); // 移除地块
            }
            //physicCheck.UpdateTilemapCollider(); // 重新组合碰撞体
            status.lastDigTime = Time.time;
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
