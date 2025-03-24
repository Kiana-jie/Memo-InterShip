using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LitJson;
using System;

public class InventoryManager : MonoBehaviour
{
    #region ����ģʽ
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();//�������ھ�̬�����У���˲���ֱ����this����gameobject����ȡ���
            }
            return _instance;
        }
    }
    #endregion
    #region ��ȡ��Ʒ��Ϣ
    /// <summary>
    /// ͨ�� ID ��ȡ��Ʒ
    /// </summary>
    /// <param name="id">��Ʒ��Ψһ ID</param>
    /// <returns>����ƥ�����Ʒ�������δ�ҵ����� null</returns>
    public Item GetItemByID(int id)
    {
        foreach (Item item in itemList)
        {
            if (item != null && item.ID == id)
            {
                return item;
            }
        }
        Debug.LogWarning("δ�ҵ� ID Ϊ " + id + " ����Ʒ��");
        return null;
    }
    #endregion


    #region ����json
    //������ƵĴ��˼���������json�ļ���һ��д�õ�װ����Ϣ,��json�ļ���һ��[{},{}]��������������Ǽ̳���Item�ģ�����趨һ��itemList����
    //��json�ļ�������itemList(����json��������Ҫ�õ�Litjson��unity���õ�jsonutility���ܽ���������)�����ʹ��itemList = JsonMapper.ToObject<List<Item>>(itemsjson);
    //�����һ�����⣬������Ȼ�ܹ��ɹ����������ݣ�������Ϊ��json���������ŵ���Item����������������ض���һЩ���ԾͲ��ܳɹ�������itemList ���ϵĶ����У���ΪitemList��ŵ���Item�����Ǹ�����󣩣����Ҳ�ͷ��ʲ���
    //��˾���Ҫ��̬����json��Ҳ���ǰ����е�json��Ϣ��Ҫ��������������ֻ��json����ΪItem������˾Ϳ���ʹ��JsonData�࣬������json�Ľ���������ͨ���ж�ÿһ�������itemtype��newItem���Ӷ��󣬲�add��itemLis��(��������ת��)�����Ҫʹ��itemList����ʱ������Ҫ����ת�ͳɶ�Ӧ���������
    private List<Item> itemList = new List<Item>();

    private void Start()
    {
        ParseItemJson();
    }
    //������Ʒ��Ϣ
    void ParseItemJson()
    {
        TextAsset itemText = Resources.Load<TextAsset>("bagData");
        string itemsjson = itemText.text;//��Ʒ��Ϣ��json��ʽ
        //itemList=JsonUtility.FromJson<List<Item>>(itemsjson);//jsonuti���ܽ���������
        JsonData jsondata = JsonMapper.ToObject(itemsjson);
        //itemList = JsonMapper.ToObject<List<Item>>(itemsjson);//��json������itemList�У���һ������List���ϱ��������������(�Ҿ������������ȱ��)
        Item itemtemp = null;
        for (int i = 0; i < jsondata.Count; i++)//����Ʒ����������
        {
            int id = (int)jsondata[i]["Id"];
            string name = jsondata[i]["Name"].ToString();
            ItemType itemType = (ItemType)((int)jsondata[i]["ItemType"]);
            string description = jsondata[i]["Description"].ToString();
            int capicity = (int)jsondata[i]["Capicity"];
            string sprite = jsondata[i]["Sprite"].ToString();
            switch (itemType)
            {
                case ItemType.Buyable:
                    int BuyPrice = (int)jsondata[i]["BuyPrice"];
                    BuyableType buyableType = (BuyableType)((int)jsondata[i]["BuyableType"]);
                    switch (buyableType)
                    {
                        case BuyableType.Consumable:
                            ConsumableType consumableType = (ConsumableType)((int)jsondata[i]["ConsumableType"]);
                            switch(consumableType)
                            {
                                case ConsumableType.RecoverWater:
                                    int HP = (int)jsondata[i]["HP"];
                                    int OP = (int)jsondata[i]["OP"];
                                    itemtemp = new RecoverWater(HP, OP, consumableType, BuyPrice, buyableType, id, name, itemType, description, capicity, sprite);
                                    break;
                                case ConsumableType.Bomb:
                                    int damage = (int)jsondata[i]["Damage"];
                                    itemtemp = new Bomb(damage, consumableType, BuyPrice, buyableType, id, name, itemType, description, capicity, sprite);
                                    break;
                                case ConsumableType.FastMove:
                                    break;
                            }
                            break;
                        case BuyableType.Equipment:
                            break;
                    }
                    break;
                case ItemType.Sellable:
                    int SellPrice = (int)jsondata[i]["SellPrice"];
                    itemtemp = new Sellable(SellPrice,id,name,itemType,description,capicity,sprite);
                    break;
                default:
                    break;
            }
            itemList.Add(itemtemp);
        }
        RecoverWater test = (RecoverWater)itemList[0];
        Debug.Log(test.HP + "+" + test.OP);
    }
    #endregion
}
