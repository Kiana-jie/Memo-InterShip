using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private int itemID;
    

    public void Initialize(int id)
    {
        itemID = id;
        
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            bool success = BackPack.Instance.StoreItem(itemID);
            //Debug.Log()
            if (success) Destroy(gameObject); // 成功拾取后销毁掉落物
        }
    }
}
