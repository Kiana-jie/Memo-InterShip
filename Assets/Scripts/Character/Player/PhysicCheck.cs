using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{
    [Header("地面检测参数")]
    public float checkRaduis;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;

    private PlayerStatus status;
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();   
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    public void Check()
    {
        status.isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
