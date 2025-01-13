using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [Header("HitBoss2 Effect")]
    public ParticleSystem HitPuple;

    public void HitPupleEffect(Vector2 hitPoint)
    {
        HitPuple.Stop();
        HitPuple.gameObject.transform.position = hitPoint;
        HitPuple.Play();
    }

}
