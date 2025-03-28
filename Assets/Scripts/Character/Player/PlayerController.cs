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
    private Animator anim;
    private CameraShake cameraShake;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        physicCheck = GetComponent<PhysicCheck>();
        tesManager = GameObject.Find("Layer-grounds").GetComponent<TilesManagement>();
        anim = GetComponent<Animator>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (status.canInput)
        {
            HandleDig();
            //Test1();
            HandleUseItems();
        }
        
    }

    

    public void Test1()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {

            int id = UnityEngine.Random.Range(1, 4);//通过生成装备id来表示生成装备
            BackPack.Instance.StoreItem(id);

        }
    }

    


    private void FixedUpdate()
    {
        if(status.canInput)
        {
            HandleMovement();
            HandleFly();
        }
        //SpeedLimit();
    }
    public void HandleMovement()
    {

        //移动动画
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(-status.speed, rb.velocity.y);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(status.speed, rb.velocity.y);
            anim.SetBool("isWalking", true);
        }
        else anim.SetBool("isWalking", false);
        

    }
    public void HandleFly()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            AudioManager.Instance.Play("fly", gameObject);
            //飞行动画
            rb.gravityScale = 0.5f;
            rb.AddForce(Vector3.up * status.flyForce, ForceMode2D.Impulse);
            anim.SetBool("isFlying", true);
            status.isFlying = true;
        }
        else
        {
            AudioManager.Instance.Stop("fly", gameObject);
            anim.SetBool("isFlying", false);
            status.isFlying = false;
        }

    }
    public void HandleDig()
    {
        //dig动画
        if (status.canDig == false) { anim.SetBool("isDigingGround", false); return; }
            
        if(Time.time - status.lastDigTime < status.digCoolDown) { return; }
        else
        {
            
            Vector3Int tilePosition = physicCheck.GetTilePosition();
            if (Input.GetKey(KeyCode.DownArrow)) {
                AudioManager.Instance.Play("drill", gameObject);
                cameraShake.StartShake();
                DigTile(tilePosition + Vector3Int.down); 
                anim.SetBool("isDigingGround", true);
                anim.SetBool("isDigingWall", false);
            }//向下挖
            else if (status.isLeftwalled == true && Input.GetKey(KeyCode.LeftArrow)) {
                AudioManager.Instance.Play("drill", gameObject);
                cameraShake.StartShake();
                DigTile(tilePosition + Vector3Int.left);
                anim.SetBool("isDigingWall", true);
                anim.SetBool("isDigGround", false);
            }//向左挖
            else if (status.isRightwalled == true && Input.GetKey(KeyCode.RightArrow)) {
                AudioManager.Instance.Play("drill", gameObject);
                cameraShake.StartShake();
                DigTile(tilePosition + Vector3Int.right);
                anim.SetBool("isDigingWall", true);
                anim.SetBool("isDigGround", false);
            }//向右挖
            else
            {
                AudioManager.Instance.Stop("drill", gameObject);
                anim.SetBool("isDigingGround", false);
                anim.SetBool("isDigingWall", false);
            }
            
                                                                                                                                                               
        }
    }
    public void HandleUseItems()
    {
        if(Input.GetKeyDown(KeyCode.Q)) { 
            BackPack.Instance.UseItem(1);
            AudioManager.Instance.Play("medicBox", gameObject);
        }
        if(Input.GetKeyDown(KeyCode.W)) {
            BackPack.Instance.UseItem(3);
            AudioManager.Instance.Play("bombInstall", gameObject);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            BackPack.Instance.UseItem(4);
            AudioManager.Instance.Play("medicalBox", gameObject);
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            BackPack.Instance.UseItem(5); 
        }
    }

    public void DigTile(Vector3Int pos)
    {
        if (physicCheck.tilemap.HasTile(pos))
        {
            //优化：只需考虑player周围的地块？
            
            tesManager.tiles[pos].health -= status.digForce;
            if (tesManager.tiles[pos].health == 7 && tesManager.tiles[pos].isOre) { tesManager.SpawnDrop(pos, tesManager.tiles[pos].itemID); }//掉落矿物
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
