using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEvent : EventTrigger
{
    public GameObject obj;
    public bool isActive;
    public override void Enter()
    {
        obj.SetActive(isActive);
    }
    public override void Execute(EventController eventController)
    {
        eventController.NextEvent();
    }
    public override void Exit()
    {
        
    }
}
