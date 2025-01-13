using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionEvent : EventTrigger
{
    public UnityEvent action;
    public override void Enter()
    {
        action?.Invoke();
    }
    public override void Execute(EventController eventController)
    {
        eventController.NextEvent();
    }
    public override void Exit()
    {

    }
}
