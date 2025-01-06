using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : MonoBehaviour
{
    public enum BossState
    {
        Pattern1 = 0,
        Pattern2 = 1,
        Pattern3 = 2,
        Pattern4 = 3,

        Chase = 4,
        Attack = 5,

    }


   
}
