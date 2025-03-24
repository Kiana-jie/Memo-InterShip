using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LitJson;
using System;

public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();//这里是在静态方法中，因此不能直接用this或者gameobject来获取组件
            }
            return _instance;
        }
    }
    #endregion
    #region 获取物品信息
    /// <summary>
    /// 通过 ID 获取物品
    /// </summary>
    /// <param name="id">物品的唯一 ID</param>
    /// <returns>返回匹配的物品对象，如果未找到返回 null</returns>
    public Item GetItemByID(int id)
    {
        foreach (Item item in itemList)
        {
            if (item != null && item.ID == id)
            {
                return item;
            }
        }
        Debug.LogWarning("未找到 ID 为 " + id + " 的物品！");
        return null;
    }
    #endregion


    #region 解析json
    //这里设计的大概思想就像，首先json文件是一个写好的装备信息,改json文件是一个[{},{}]，数组里面的类是继承自Item的，因此设定一个itemList集合
    //将json文件解析到itemList(解析json到数组需要用到Litjson，unity内置的jsonutility不能解析成数组)，如果使用itemList = JsonMapper.ToObject<List<Item>>(itemsjson);
    //会出现一个问题，就是虽然能够成功解析到内容，但是因为是json数组里面存放的是Item的子类对象，子类有特定的一些属性就不能成功解析到itemList 集合的对象中（因为itemList存放的是Item对象，是父类对象），因此也就访问不到
    //因此就需要动态解析json，也就是把所有的json信息都要解析出来，不能只将json解析为Item对象，因此就可以使用JsonData类，来接受json的解析，并且通过判断每一个对象的itemtype来newItem的子对象，并add到itemLis中(子类向上转型)；如果要使用itemList对象时，就需要向下转型成对应的子类对象
    private List<Item> itemList = new List<Item>();

    private void Start()
    {
        ParseItemJson();
    }
    //解析物品信息
    void ParseItemJson()
    {
        TextAsset itemText = Resources.Load<TextAsset>("bagData");
        string itemsjson = itemText.text;//物品信息的json格式
        //itemList=JsonUtility.FromJson<List<Item>>(itemsjson);//jsonuti不能解析成数组
        JsonData jsondata = JsonMapper.ToObject(itemsjson);
        //itemList = JsonMapper.ToObject<List<Item>>(itemsjson);//将json解析到itemList中，用一个父类List集合保存所有子类对象(我觉得这里有设计缺陷)
        Item itemtemp = null;
        for (int i = 0; i < jsondata.Count; i++)//用物品类型来区分
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
