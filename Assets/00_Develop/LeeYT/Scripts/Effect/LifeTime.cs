using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(HitEffect());
    }

    private void Start()
    {
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        yield return new WaitForSeconds(1f);
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
    }
}
