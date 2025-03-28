using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBgm : MonoBehaviour
{

    private void Start()
    {
        AudioManager.Instance.Play("normalBgm", gameObject);
    }
}
