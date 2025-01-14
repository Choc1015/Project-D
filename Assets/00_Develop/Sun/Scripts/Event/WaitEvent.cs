using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitEvent : EventTrigger
{
    public float waitTimer;
    public override void Enter()
    {

    }
    public override void Execute(EventController eventController)
    {
        if (waitTimer <= 0)
            eventController.NextEvent();
        else
            waitTimer -= Time.deltaTime;
    }
    public override void Exit()
    {

    }
}
