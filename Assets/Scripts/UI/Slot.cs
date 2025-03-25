using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;
    private Outline outline;
    private Image slotImage;

    private void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null )
        {
            outline.enabled = false;
        }
        slotImage = GetComponent<Image>();
    }
    public void StoreItem(Item item)
    {
        if(transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab);
            itemGameObject.transform.SetParent(transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }

    public int GetItemID() { /*Debug.Log(transform.GetChild(0).name);*/ return transform.GetChild(0).GetComponent<ItemUI>().Item.ID; }
        
    public int GetShopItemID() { return transform.GetChild(0).GetComponent<ShopItemUI>().itemID; }    
    
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capicity;       
    }

    public void OnSlotClicked()
    {
        if (outline != null)
        {
            outline.enabled = true;  // ���� Outline ����
        }
        // ������֪ͨ `Shop` ��ǰѡ���������
        Shop.Instance.SetSelectedSlot(this);  // �� Shop �м�¼��ǰѡ��� Slot
    }

    public void DeselectSlot()
    {
        if (outline != null)
        {
            outline.enabled = false;  // �ر� Outline ����
        }
    }
}
