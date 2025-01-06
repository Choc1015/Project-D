using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyStateMachine
{
    protected override void Initialize()
    {
        base.Initialize();

        Debug.Log("Boss");

    }



}
