using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeEvent : EventTrigger
{
    public bool isActive;
    public float alpha, timer, delayTimer;
    public Color fadeColor;
    public override void Enter()
    {
        UIManager.Instance.SetActiveFadeImage(isActive, alpha, timer, fadeColor);
        timer += delayTimer;
    }
    public override void Execute(EventController eventController)
    {
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            eventController.NextEvent();
        }
    }
    public override void Exit()
    {
        
    }
}
