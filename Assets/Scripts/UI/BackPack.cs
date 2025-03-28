using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackPack : Inventory
{
    public TextMeshProUGUI moneyText;
    #region 单例模式
    private static BackPack _instance;
    public static BackPack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Panel_backpack").GetComponent<BackPack>();//这里是在静态方法中，因此不能直接用this或者gameobject来获取组件
            }
            return _instance;
        }
    }
    #endregion
    
    
    
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            SellAllSellableItems();
        }*/
        if(Input.GetKeyDown(KeyCode.B))
        {
           ShowBackpackPanel();
        }
    }

    public void SellAllSellableItems()
    {
        int totalEarnings = 0;

        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0) // 确保槽内有物品
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                Item item = itemUI.Item;

                if (item is Sellable sellableItem)
                {
                    int sellAmount = itemUI.Amount; // 记录当前物品数量
                    totalEarnings += sellableItem.SellPrice * sellAmount;
                    itemUI.ReduceAmount(sellAmount); // 正确减少物品数量（内部处理清除）
                }
            }
        }

        if (totalEarnings > 0)
        {
            PlayerStatus.Instance.AddMoney(totalEarnings);
            UpdateMoneyUI();
            Debug.Log($"成功卖出矿物，获得金币: {totalEarnings}");
        }
        else
        {
            Debug.Log("背包中没有可出售的矿物");
        }
    }
    public void UpdateMoneyUI()
    {
        int money = PlayerStatus.Instance.money;
        moneyText.text = $"{money}";
    }
    public bool UseItem(int itemId)
    {
        // 查找玩家背包中是否有该道具
        Slot slot = slotList.Find(s => s.GetItemID() == itemId);
        if (slot != null)
        {
            ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
            Item item = itemUI.Item;
            if (item is Consumable ConsumableItem)
            {
                // 执行道具的使用逻辑
                ConsumableItem.Consume();
                itemUI.ReduceAmount(); // 移除物品
                return true;
            }
            else
            {
                Debug.LogWarning("该物品不能使用！");
            }
        }
        else
        {
            AudioManager.Instance.Play("error", gameObject);
            Debug.LogWarning("未找到该道具！");
        }
        return false;
    }
}
