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

}
