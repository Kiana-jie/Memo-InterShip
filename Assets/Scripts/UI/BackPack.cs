using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : Inventory
{
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            SellAllSellableItems();
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
            Debug.Log($"成功卖出矿物，获得金币: {totalEarnings}");
        }
        else
        {
            Debug.Log("背包中没有可出售的矿物");
        }
    }
}
