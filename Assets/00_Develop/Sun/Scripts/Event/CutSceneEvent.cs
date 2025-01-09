using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEvent : EventTrigger
{
    public Sprite sprite;
    public override void Enter()
    {
        UIManager.Instance.SetCutSceneImage(sprite);
        
    }
    public override void Execute(EventController eventController)
    {
        eventController.NextEvent();
    }
    public override void Exit()
    {

    }
}
