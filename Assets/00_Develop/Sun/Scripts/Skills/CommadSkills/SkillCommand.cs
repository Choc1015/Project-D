using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class SkillCommand 
{
    public string Name;
    public List<KeyCode> command = new();
    private List<KeyCode> inputList = new();
    public UnityEvent action, exitAction;

    public float commandInputTimeLimit = 1.0f; 
    private float timer;

    public bool isKeyDown, isHold;
    private bool trigger;
    public float nextAction;

    private SkillCommandController controller;

    public bool isDisable;
    public void Init(SkillCommandController controller)
    {
        if(!this.controller)
            this.controller = controller;

        if (isKeyDown)
            controller.skillEvents += InputCommandKeyDown;
        else
            controller.skillEvents += InputCommandKey;

        controller.skillEvents += CheckCommand;
        controller.skillEvents += ClearExpiredInput;
        controller.skillEvents += InputCommandKeyUp;
    }
    public void InputCommandKey()
    {
        if (isDisable)
            return;

        if (Input.anyKey)
        {
            foreach (KeyCode keyCode in command)
            {
                if (Input.GetKey(keyCode))
                {
                    inputList.Add(keyCode);
                    timer = 0;
                }
            }
        }
    }
    public void InputCommandKeyDown()
    {
        if (isDisable)
            return;


        if (Input.anyKeyDown && controller.CanAction)
        {
            foreach (KeyCode keyCode in command)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    inputList.Add(keyCode);
                    timer = 0;
                }
            }
        }
    }
    public void InputCommandKeyUp()
    {
        if (isDisable)
            return;


        if (nextAction == 0 && controller.CanAction)
        {
            if (!StopCommand())
            {
                controller.UsingCommand(nextAction, this);
                Debug.Log(Name);
            }
        }


    }
    public void CheckCommand()
    {
        if (trigger)
        {
            action?.Invoke();
            controller.UsingCommand(nextAction, this);
            trigger = StopCommand();
            return;
        }

        if (!controller.CanAction)
            return;
        
        if (IsCommandMatched(command))
        {
            if (isHold)
                trigger = true;
            action?.Invoke();

            if (nextAction > 0)
                controller.UsingCommand(nextAction, this);

            inputList.Clear(); // 성공 시 입력 버퍼 초기화
        }
    }
    private bool StopCommand()
    {
        foreach (KeyCode keyCode in command)
        {
            if (Input.GetKeyUp(keyCode))
            {
                return false;
            }
        }
        return true;
    }
    private bool IsCommandMatched(List<KeyCode> command)
    {
        if (inputList.Count < command.Count) return false;

        for (int i = 0; i < command.Count; i++)
        {
            if (inputList[inputList.Count - command.Count + i] != command[i])
            {
                return false;
            }
        }
        return true;
    }
    public void ClearExpiredInput()
    {
        if (isDisable)
            return;

        timer += Time.deltaTime;
        if (timer > commandInputTimeLimit)
        {
            inputList.Clear();
        }
    }
}
