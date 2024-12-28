using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackInfo
{
    public Vector3 dir;
    public float power;
    public float knockBackTime;
    public float stunTime;
    public bool isStun;
    public KnockBackInfo(Vector3 dir, float power, float knockBackTimer, float stunTime)
    {
        this.dir = dir;
        this.power = power;
        this.knockBackTime = knockBackTimer;
        this.stunTime = stunTime;
        if (stunTime >= 2)
            isStun = true;
        else
            isStun = false;
    }
}
