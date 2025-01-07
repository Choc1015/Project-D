using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxEvent : EventTrigger
{
    public string contents;
    public override void Enter()
    {
        UIManager.Instance.textBox.SetTextBox(contents);
    }
    public override void Excute(EventController eventController)
    {
        if (!UIManager.Instance.textBox.gameObject.activeInHierarchy)
        {
            eventController.NextEvent();
        }
    }
    public override void Exit()
    {
        
    }
}
