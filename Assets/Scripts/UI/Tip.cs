using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tip : MonoBehaviour
{
    private TextMeshProUGUI pickupTip;
    // Start is called before the first frame update
    private void Start()
    {
        pickupTip = GetComponent<TextMeshProUGUI>();
    }
    public void ShowTip(string message)
    {
        if (pickupTip != null)
        {
            pickupTip.text = message;
            StartCoroutine(HideTipAfterDelay());
        }
    }

    private IEnumerator HideTipAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Hia");
        pickupTip.text = "";
    }
}
