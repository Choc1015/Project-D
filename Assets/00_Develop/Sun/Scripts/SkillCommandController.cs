using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillCommandController : MonoBehaviour
{
    public SkillCommand[] skillCommands;
    public Action skillEvents;
    public UnityEvent exitEvents;

    private float nextAction;
    public bool CanAction;
    void Start()
    {
        foreach(SkillCommand skillCommand in skillCommands)
        {
            skillCommand.Init(this);
        }
        
    }

    
    public void ControllerAction()
    {
        skillEvents?.Invoke();

        nextAction -= Time.deltaTime;
        CanAction = nextAction <= 0 ? true : false;
        SetCanAction();

    }
    public void UsingCommand(float nextAction)
    {
        this.nextAction = nextAction;
        SetCanAction();
        CancelInvoke();
        Invoke("ExitAction", nextAction);
    }

    private void SetCanAction()
    {
        CanAction = nextAction <= 0 ? true : false;
    }

    public void ExitAction()
    {
        exitEvents?.Invoke();

    }
}
