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
    
    public string Sprite { get; set; }//�����Ʒ��ͼƬ·����ͨ��Resources����
    public Item(int iD, string name, ItemType itemType, string description, /*int capicity*/ string sprite)
    {
        ID = iD;
        Name = name;
        ItemType = itemType;
        Description = description;
        //Capicity = capicity;
        Sprite = sprite;
    }
    public Item() { }//�޲ι��캯������ֹ������û��û����ʽ���幹�캯�������Ĭ�ϵ��ø����޲�������
//������

}