using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Buyable,
    Sellable
}
public class Item
{
    
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType ItemType { get; set; }
    public string Description { get; set; }
    //public int Capicity { get; set; }
    
    public string Sprite { get; set; }//存放物品的图片路径，通过Resources加载
    public Item(int iD, string name, ItemType itemType, string description, /*int capicity*/ string sprite)
    {
        ID = iD;
        Name = name;
        ItemType = itemType;
        Description = description;
        //Capicity = capicity;
        Sprite = sprite;
    }
    public Item() { }//无参构造函数，防止子类在没中没有显式定义构造函数，则会默认调用父类无参数构造
//函数。

}