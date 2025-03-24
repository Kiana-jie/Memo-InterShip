using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : Inventory
{
    #region 单例模式
    private static BackPack _instance;
    public static BackPack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Panel_backpack").GetComponent<BackPack>();//这里是在静态方法中，因此不能直接用this或者gameobject来获取组件
            }
            return _instance;
        }
    }
    #endregion

}
