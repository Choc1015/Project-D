using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillCommandController : MonoBehaviour
{
    public SkillCommand[] skillCommands;
    private SkillCommand curCommand;
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
        SetCanAction();

    }
    public void UsingCommand(float nextAction, SkillCommand skillCommand)
    {
        this.nextAction = nextAction;
        curCommand = skillCommand;
        SetCanAction();
        CancelInvoke();
        Invoke("ExitAction", nextAction+0.1f);
    }

    private void SetCanAction()
    {
        CanAction = nextAction <= 0 ? true : false;
    }

    public void ExitAction()
    {
        exitEvents?.Invoke();
        curCommand?.exitAction?.Invoke();
        curCommand = null;
        Debug.Log("Stop");
    }
}
