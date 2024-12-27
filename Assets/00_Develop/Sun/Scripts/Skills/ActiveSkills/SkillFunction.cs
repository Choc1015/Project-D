using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class SkillFunction : MonoBehaviour
{
    private SkillFunctionController controller;
    public SkillCommand command;
    public GameObject skillObj;
    public float invokeTimer;
    public void Init(SkillFunctionController controller)
    {
        this.controller = controller;
        command.action.AddListener(()=> controller.player.playerState.ChangeState(PlayerState.Animation));
        command.action.AddListener(() => Invoke("InvokeAction", invokeTimer));
        command.Init(controller.commandController);
    }

    public void InvokeAction()
    {
        controller.player.playerState.ChangeState(PlayerState.Idle);
    }
}
