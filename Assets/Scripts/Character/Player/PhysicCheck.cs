using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PhysicCheck : MonoBehaviour
{
    [Header("地面检测参数")]
    public float checkRaduis;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    
    [Header("墙壁检测参数")]
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    private PlayerStatus status;

    public Tilemap tilemap; // 引用 Tilemap
    public TilemapCollider2D tilemapCollider;
    private CompositeCollider2D compositeCollider;
    // Start is called before the first frame update
    private void Awake()
    {
        tilemap = GameObject.Find("Layer-grounds").GetComponent<Tilemap>();
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        compositeCollider = tilemap.GetComponent<CompositeCollider2D>();
        status = GetComponent<PlayerStatus>();
    }
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    public void Check()
    {
        status.isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        status.isLeftwalled = Physics2D.OverlapCircle((Vector2)transform.position+leftOffset, checkRaduis, groundLayer);
        status.isRightwalled = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }

    public Vector3Int GetTilePosition()
    {
        Vector2 worldPos = transform.position;
        return tilemap.WorldToCell(worldPos);
    }

    public void UpdateTilemapCollider()
    {
        tilemapCollider.enabled = false; // 先禁用
        //compositeCollider.enabled = false;
        tilemapCollider.enabled = true;  // 再启用，让 CompositeCollider2D 重新组合
        //compositeCollider.enabled = true;
    }
}
