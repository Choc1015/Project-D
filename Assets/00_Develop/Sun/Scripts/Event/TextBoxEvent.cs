using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxEvent : EventTrigger
{
    public string contents;
    public string posObjectTag;
    public Transform posObject;
    public Vector3 basePos;
    public Vector3 offset;
    public override void Enter()
    {
        UIManager.Instance.textBox.SetTextBox(contents);
        //if(posObjectTag != default)
        //{
        //    posObject = GameObject.FindWithTag(posObjectTag).transform;
        //}
        if (posObject != default)
        {
            basePos = transform.position;
            
        }
        UIManager.Instance.textBox.transform.position = Camera.main.WorldToScreenPoint(basePos + offset);
    }
    public override void Execute(EventController eventController)
    {
        

        if (!UIManager.Instance.textBox.gameObject.activeInHierarchy)
        {
            eventController.NextEvent();
        }
    }
    public override void Exit()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }
}
