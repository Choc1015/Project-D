using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KnockBackInfo
{
    public Vector3 dir;
    public float power;
    public float knockBackTime;
    public float stunTime;
    public bool isKnockBack;
    public KnockBackInfo(Vector3 dir, float power, float stunTime, float knockBackTimer)
    {
        this.dir = dir;
        this.power = power;
        this.knockBackTime = knockBackTimer;
        this.stunTime = stunTime;
        if (stunTime >= 2)
            isKnockBack = true;
        else
            isKnockBack = false;
    }
    public void ResetValue()
    {
        dir = Vector3.zero;
        power = 0;
        knockBackTime = 0;
        stunTime = 0;
        isKnockBack = false;
    }
}
