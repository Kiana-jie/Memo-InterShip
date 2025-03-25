using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : Inventory
{
    #region ����ģʽ
    private static BackPack _instance;
    public static BackPack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Panel_backpack").GetComponent<BackPack>();//�������ھ�̬�����У���˲���ֱ����this����gameobject����ȡ���
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
            if (slot.transform.childCount > 0) // ȷ����������Ʒ
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                Item item = itemUI.Item;

                if (item is Sellable sellableItem)
                {
                    int sellAmount = itemUI.Amount; // ��¼��ǰ��Ʒ����
                    totalEarnings += sellableItem.SellPrice * sellAmount;
                    itemUI.ReduceAmount(sellAmount); // ��ȷ������Ʒ�������ڲ����������
                }
            }
        }

        if (totalEarnings > 0)
        {
            PlayerStatus.Instance.AddMoney(totalEarnings);
            Debug.Log($"�ɹ����������ý��: {totalEarnings}");
        }
        else
        {
            Debug.Log("������û�пɳ��۵Ŀ���");
        }
    }
    public bool UseItem(int itemId)
    {
        // ������ұ������Ƿ��иõ���
        Slot slot = slotList.Find(s => s.GetItemID() == itemId);
        if (slot != null)
        {
            ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
            Item item = itemUI.Item;
            if (item is Consumable ConsumableItem)
            {
                // ִ�е��ߵ�ʹ���߼�
                ConsumableItem.Consume();
                itemUI.ReduceAmount(); // �Ƴ���Ʒ
                return true;
            }
            else
            {
                Debug.LogWarning("����Ʒ����ʹ�ã�");
            }
        }
        else
        {
            Debug.LogWarning("δ�ҵ��õ��ߣ�");
        }
        return false;
    }
}
