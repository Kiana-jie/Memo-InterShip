using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Inventory
{
    private Slot selectedSlot;
    #region 单例模式
    private static Shop _instance;
    public static Shop Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Panel_shop").GetComponent<Shop>(); // 获取商店面板
            }
            return _instance;
        }
    }
    #endregion

    public void SetSelectedSlot(Slot slot)
    {
        // 如果当前已经有选中的槽，取消高亮
        if (selectedSlot != null)
        {
            selectedSlot.DeselectSlot();
        }

        // 设置新的选中槽
        selectedSlot = slot;
    }

    // 购买按钮点击事件
    public void OnBuyButtonClicked()
    {
        if (selectedSlot != null)
        {
            int itemId = selectedSlot.GetShopItemID();  // 使用 GetItemID 获取物品 ID
            Item item = InventoryManager.Instance.GetItemByID(itemId);  // 获取物品
            
            if (item != null && item is Buyable buyableItem)
            {
                BuyItem(buyableItem.ID);  // 调用购买物品的逻辑
            }
            else
            {
                Debug.LogWarning("没有选中的物品或物品不可购买");
            }
        }
    }

    private void Update()
    {
        
    }

    // 商店界面显示与隐藏
    public void ShowShopPanel()
    {
        if (canvasGroup != null)
        {
            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // 切换透明度
            canvasGroup.interactable = !isVisible;  // 切换交互性
            if (!isVisible) {
                AudioManager.Instance.Play("invenOpen", gameObject);
                AudioManager.Instance.Stop("normalBgm", gameObject);
                AudioManager.Instance.Play("shopBgm", gameObject);
            }
            else
            {
                AudioManager.Instance.Play("invenClose", gameObject);
                AudioManager.Instance.Play("normalBgm", gameObject);
                AudioManager.Instance.Stop("shopBgm", gameObject);
            }
        }
    }
    public bool BuyItem(int itemID)
    {
        Item item = InventoryManager.Instance.GetItemByID(itemID); // 从物品管理器获取物品
        if (item == null || !(item is Buyable)) // 如果物品不存在或不是可以买的物品
        {
            Debug.LogWarning("无法购买该物品");
            return false;
        }

        Buyable buyableItem = (Buyable)item;

        // 检查玩家是否有足够的金币
        if (PlayerStatus.Instance.money >= buyableItem.BuyPrice)
        {
            // 购买物品
            PlayerStatus.Instance.AddMoney(-buyableItem.BuyPrice);  // 扣除金币
            //Debug.Log(StoreItem(itemID));  // 把物品存入背包
            BackPack.Instance.StoreItem(itemID);
            AudioManager.Instance.Play("buy", gameObject);
            Debug.Log($"成功购买 {item.Name}，花费 {buyableItem.BuyPrice} 金币");
            return true;
        }
        else
        {
            AudioManager.Instance.Play("error", gameObject);
            Debug.LogWarning("金币不足，无法购买该物品");
            return false;
        }
    }
}
