using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : EventTrigger
{
    public override void Enter()
    {
        
    }
    public override void Execute(EventController eventController)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            eventController.NextEvent();
    }
    public override void Exit()
    {

    }
}
