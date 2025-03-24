using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; private set; }
    public int Amount { get; private set; }

    private Image itemImage;//item��ͼƬ������
    private Text amountText;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        amountText = GetComponentInChildren<Text>();
    }

    public void SetItem(Item item, int amount = 1)//�ò���item����itemUI�е�item
    {
        this.Item = item;
        this.Amount = amount;
        //����UI
        itemImage.sprite = Resources.Load<Sprite>(item.Sprite);//����item�е�sprite����Resources�ļ��е�ͼƬ����ֵ����ǰslot�е�item
        if (item.Capicity > 1)//��������1����ʾ
            amountText.text = Amount.ToString();
        else
            amountText.text = "";
    }
    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        //����UI
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
