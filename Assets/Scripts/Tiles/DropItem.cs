using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropItem : MonoBehaviour
{
    private int itemID;
    private string itemName;
    private int itemPrice;
    private float tipDuration = 2f;
    private Tip tip;
    
    public void Initialize(int id)
    {
        itemID = id;
        
    }

    private void Start()
    {
        tip = GameObject.Find("PickupTip").GetComponent<Tip>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            bool success = BackPack.Instance.StoreItem(itemID);
            //Debug.Log()
            if (success)
            {
                AudioManager.Instance.Play("pickUp", gameObject);
                //��Ϸ�ڵ���tip:��ʯ�Ľ�Ǯ:
                ShowItemInfo(itemID);
                Destroy(gameObject); // �ɹ�ʰȡ�����ٵ�����
            }
        }
    }

    public void ShowItemInfo(int id)
    {
        Item item = InventoryManager.Instance.GetItemByID(id);
        if (item != null)
        {
            string itemName = item.Name;
            string itemInfo = $" {itemName} ";

            if (item is Sellable sellableItem) // �ж��Ƿ��� Sellable ����
            {
                int sellPrice = sellableItem.SellPrice;
                itemInfo += $" :{sellPrice} $";
            }

            
            tip.ShowTip(itemInfo); // �������ǰ��д�� ShowTip ����
        }
    }

    


}
