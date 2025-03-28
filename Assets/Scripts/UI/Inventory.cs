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
        slotList.AddRange(GetComponentsInChildren<Slot>());//可能有bug
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
            Debug.LogWarning("要储存的物品id不存在");
            return false;
        }
        //如果要存储的物品id存在
        if (item.Capicity == 1)//如果容量为1
        {
            //放在一个新格子里面
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);//把物品存储到空物品槽里面
            }
        }
        else//如果容量不为1
        {
            //找一个和item的id一样的格子进行存放
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
                    Debug.LogWarning("没有空的物品槽");
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
            canvasGroup.alpha = isVisible ? 0 : 1;  // 切换透明度
            canvasGroup.interactable = !isVisible;  // 切换交互性
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


