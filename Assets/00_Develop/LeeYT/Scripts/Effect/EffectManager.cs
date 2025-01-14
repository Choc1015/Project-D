using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [Header("HitBoss2 Effect")]
    public GameObject hitPuple;
    public GameObject healEffect;
    public GameObject weekEffect;
    public GameObject strongEffect;

    [Header("DragonHit Boss")]
    public GameObject HitDragon;

    public void PlayHitPupleEffect(Vector2 hitPoint)
    {
        if (hitPuple == null)
        {
            Debug.LogWarning("HitPuple GameObject is not assigned!");
            return;
        }

        ObjectPoolManager.Instance.SpawnFromPool(hitPuple.name, hitPoint);
    }
    public void PlayHealEffect(Vector2 hitPoint)
    {
        if (healEffect == null)
        {
            Debug.LogWarning("healEffect GameObject is not assigned!");
            return;
        }

        ObjectPoolManager.Instance.SpawnFromPool(healEffect.name, hitPoint);
    }
    public void PlayWeekEffect(Vector2 hitPoint)
    {
        if (weekEffect == null)
        {
            Debug.LogWarning("weekEffect GameObject is not assigned!");
            return;
        }

        ObjectPoolManager.Instance.SpawnFromPool(weekEffect.name, hitPoint);
    }
    public void PlayStrongEffect(Vector2 hitPoint)
    {
        if (strongEffect == null)
        {
            Debug.LogWarning("strongEffect GameObject is not assigned!");
            return;
        }

        ObjectPoolManager.Instance.SpawnFromPool(strongEffect.name, hitPoint);
    }
    public void PlayDragonHitEffect(Vector2 hitPoint)
    {
        if (HitDragon == null)
        {
            Debug.LogWarning("HitDragon GameObject is not assigned!");
            return;
        }

        ObjectPoolManager.Instance.SpawnFromPool(HitDragon.name, hitPoint);
    }

}
