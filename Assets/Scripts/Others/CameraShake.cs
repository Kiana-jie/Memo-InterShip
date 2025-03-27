using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originPos;
    public float shakeStrength;
    public float shakeTime;
    

    // Update is called once per frame
   public void StartShake()
    {
        originPos = transform.position;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0;
        while (elapsed < shakeTime) 
        {
            Vector3 randomOffset = Random.insideUnitCircle * shakeStrength;
            transform.position = originPos + randomOffset;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originPos;
    }
}
