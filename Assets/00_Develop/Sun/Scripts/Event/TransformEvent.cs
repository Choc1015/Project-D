using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEvent : EventTrigger
{
    public Transform target;
    public Vector3 startPos, endPos;
    public float timer, delayTimer;
    public bool InCanvas;

    private Vector3 startPosTemp, endPosTemp;
    public override void Enter()
    {
        target.gameObject.SetActive(true);
        if(InCanvas)
        {
            startPosTemp = Camera.main.WorldToScreenPoint(startPos);
            endPosTemp = Camera.main.WorldToScreenPoint(endPos);
        }
        else
        {
            startPosTemp = startPos;
            endPosTemp = endPos;
        }
        target.transform.position = startPosTemp;
        target.DOMove(endPosTemp, timer, true);
        timer += delayTimer;
    }
    public override void Execute(EventController eventController)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            eventController.NextEvent();
        }
    }
    public override void Exit()
    {

    }
}
