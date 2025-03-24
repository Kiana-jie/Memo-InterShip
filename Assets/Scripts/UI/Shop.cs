using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Inventory
{
    private Slot selectedSlot;
    #region ����ģʽ
    private static Shop _instance;
    public static Shop Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Panel_shop").GetComponent<Shop>(); // ��ȡ�̵����
            }
            return _instance;
        }
    }
    #endregion

    public void SetSelectedSlot(Slot slot)
    {
        // �����ǰ�Ѿ���ѡ�еĲۣ�ȡ������
        if (selectedSlot != null)
        {
            selectedSlot.DeselectSlot();
        }

        // �����µ�ѡ�в�
        selectedSlot = slot;
    }

    // ����ť����¼�
    public void OnBuyButtonClicked()
    {
        if (selectedSlot != null)
        {
            int itemId = selectedSlot.GetItemID();  // ʹ�� GetItemID ��ȡ��Ʒ ID
            Item item = InventoryManager.Instance.GetItemByID(itemId);  // ��ȡ��Ʒ

            if (item != null && item is Buyable buyableItem)
            {
                BuyItem(buyableItem.ID);  // ���ù�����Ʒ���߼�
            }
            else
            {
                Debug.LogWarning("û��ѡ�е���Ʒ����Ʒ���ɹ���");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // ���� S ����ʾ/�����̵����
        {
            ShowShopPanel();
        }
    }

    // �̵������ʾ������
    public void ShowShopPanel()
    {
        if (canvasGroup != null)
        {
            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // �л�͸����
            canvasGroup.interactable = !isVisible;  // �л�������
        }
    }
    public bool BuyItem(int itemID)
    {
        Item item = InventoryManager.Instance.GetItemByID(itemID); // ����Ʒ��������ȡ��Ʒ
        if (item == null || !(item is Buyable)) // �����Ʒ�����ڻ��ǿ��������Ʒ
        {
            Debug.LogWarning("�޷��������Ʒ");
            return false;
        }

        Buyable buyableItem = (Buyable)item;

        // �������Ƿ����㹻�Ľ��
        if (PlayerStatus.Instance.money >= buyableItem.BuyPrice)
        {
            // ������Ʒ
            PlayerStatus.Instance.AddMoney(-buyableItem.BuyPrice);  // �۳����
            StoreItem(itemID);  // ����Ʒ���뱳��
            Debug.Log($"�ɹ����� {item.Name}������ {buyableItem.BuyPrice} ���");
            return true;
        }
        else
        {
            Debug.LogWarning("��Ҳ��㣬�޷��������Ʒ");
            return false;
        }
    }
}
