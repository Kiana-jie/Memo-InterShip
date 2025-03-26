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
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        physicCheck = GetComponent<PhysicCheck>();
        tesManager = GameObject.Find("Layer-grounds").GetComponent<TilesManagement>();
        anim = GetComponent<Animator>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleDig();
        //Test1();
        HandleUseItems();
    }

    

    public void Test1()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {

            int id = UnityEngine.Random.Range(1, 4);//ͨ������װ��id����ʾ����װ��
            BackPack.Instance.StoreItem(id);

        }
    }

    


    private void FixedUpdate()
    {
        HandleMovement();
        HandleFly();
        //SpeedLimit();
    }
    public void HandleMovement()
    {

        //�ƶ�����
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
            //���ж���
            rb.AddForce(Vector3.up * status.flyForce, ForceMode2D.Impulse);
            anim.SetBool("isFlying", true);
        }
        else anim.SetBool("isFlying", false);
        

    }
    public void HandleDig()
    {
        //dig����
        if (status.canDig == false) { anim.SetBool("isDigingGround", false); return; }
            
        if(Time.time - status.lastDigTime < status.digCoolDown) { return; }
        else
        {
            
            Vector3Int tilePosition = physicCheck.GetTilePosition();
            if (Input.GetKey(KeyCode.DownArrow)) {
                DigTile(tilePosition + Vector3Int.down); 
                anim.SetBool("isDigingGround", true);
                anim.SetBool("isDigingWall", false);
            }//������
            else if (status.isLeftwalled == true && Input.GetKey(KeyCode.LeftArrow)) {
                DigTile(tilePosition + Vector3Int.left);
                anim.SetBool("isDigingWall", true);
                anim.SetBool("isDigGround", false);
            }//������
            else if (status.isRightwalled == true && Input.GetKey(KeyCode.RightArrow)) {
                DigTile(tilePosition + Vector3Int.right);
                anim.SetBool("isDigingWall", true);
                anim.SetBool("isDigGround", false);
            }//������
            else
            {
                anim.SetBool("isDigingGround", false);
                anim.SetBool("isDigingWall", false);
            }
            
                                                                                                                                                               
        }
    }
    public void HandleUseItems()
    {
        if(Input.GetKeyDown(KeyCode.Q)) { BackPack.Instance.UseItem(1); }
        if(Input.GetKeyDown(KeyCode.W)) { BackPack.Instance.UseItem(3); }
    }

    public void DigTile(Vector3Int pos)
    {
        if (physicCheck.tilemap.HasTile(pos))
        {
            //�Ż���ֻ�迼��player��Χ�ĵؿ飿
            
            tesManager.tiles[pos].health -= status.digForce;
            if (tesManager.tiles[pos].health == 7 && tesManager.tiles[pos].isOre) { tesManager.SpawnDrop(pos, tesManager.tiles[pos].itemID); }//�������
            tesManager.PlayDamageAnimation(pos);
            if (tesManager.tiles[pos].health <= 0)
            {
                physicCheck.tilemap.SetTile(pos, null); // �Ƴ��ؿ�
            }
            //physicCheck.UpdateTilemapCollider(); // ���������ײ��
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
