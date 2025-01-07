using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyStateMachine;

public class SunObj : Human
{
    // Start is called before the first frame update
    void Start()
    {
        statController.Init();
        PatternManager.Instance.IsSunAlive = true;
    }

    protected override void DieHuman()
    {
        PatternManager.Instance.IsSunAlive = false;
        base.DieHuman();
    }

}
