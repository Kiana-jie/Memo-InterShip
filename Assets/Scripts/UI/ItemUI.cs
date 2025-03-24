using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; private set; }
    public int Amount { get; private set; }

    private Image itemImage;//item中图片和数量
    private Text amountText;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        amountText = GetComponentInChildren<Text>();
    }

    public void SetItem(Item item, int amount = 1)//用参数item设置itemUI中的item
    {
        this.Item = item;
        this.Amount = amount;
        //更新UI
        itemImage.sprite = Resources.Load<Sprite>(item.Sprite);//根据item中的sprite加载Resources文件中的图片并赋值给当前slot中的item
        if (item.Capicity > 1)//容量大于1才显示
            amountText.text = Amount.ToString();
        else
            amountText.text = "";
    }
    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        //更新UI
        amountText.text = Amount.ToString();
    }

    public void ReduceAmount(int amount = 1)
    {
        this.Amount -= amount;
        if(this.Amount <= 0)
        {
            Destroy(gameObject);
            return;
        }
        amountText.text = Amount.ToString();

    }

}
