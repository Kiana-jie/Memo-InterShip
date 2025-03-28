using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> slotList = new List<Slot>();
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    public virtual void Start()
    {
        slotList.AddRange(GetComponentsInChildren<Slot>());//������bug
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }

    // Update is called once per frame
    
    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemByID(id);
        if (item == null)
        {
            Debug.LogWarning("Ҫ�������Ʒid������");
            return false;
        }
        //���Ҫ�洢����Ʒid����
        if (item.Capicity == 1)//�������Ϊ1
        {
            //����һ���¸�������
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("û�пյ���Ʒ��");
                return false;
            }
            else
            {
                slot.StoreItem(item);//����Ʒ�洢������Ʒ������
            }
        }
        else//���������Ϊ1
        {
            //��һ����item��idһ���ĸ��ӽ��д��
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("û�пյ���Ʒ��");
                    return false;
                }
            }
        }
        //Debug.Log(item.Name);
        return true;
    }

    private Slot FindEmptySlot()
    {
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount==0)
            {
                return slot;
            }
        }
        return null;
    }

    private Slot FindSameIdSlot(Item item)
    {
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount >= 1 && slot.GetItemID() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    public void ShowBackpackPanel()
    {
        if(canvasGroup != null)
        {

            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // �л�͸����
            canvasGroup.interactable = !isVisible;  // �л�������
            if(!isVisible )
            {
                AudioManager.Instance.Play("invenOpen", gameObject);
            }
            else
            {
                AudioManager.Instance.Play("invenClose", gameObject);
                
            }

        }
    }
}


