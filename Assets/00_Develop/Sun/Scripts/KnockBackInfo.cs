using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackInfo
{
    public Vector3 dir;
    public float power;
    public float knockBackTime;
    public float stunTime;
    public bool isLKnockBack;
    public KnockBackInfo(Vector3 dir, float power, float stunTime, float knockBackTimer)
    {
        this.dir = dir;
        this.power = power;
        this.knockBackTime = knockBackTimer;
        this.stunTime = stunTime;
        if (stunTime >= 2)
            isLKnockBack = true;
        else
            isLKnockBack = false;
    }
}
