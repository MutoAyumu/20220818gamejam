using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(EffectInterval());
    }
    IEnumerator EffectInterval()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
