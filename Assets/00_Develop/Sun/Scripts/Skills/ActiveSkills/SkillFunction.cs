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
    public float skillCooldown;
    public void Init(SkillFunctionController controller)
    {
        this.controller = controller;
        
        command.action.AddListener(() => controller.player.GetPlayerState().ChangeState(PlayerState.Animation));
        command.action.AddListener(() => Invoke("InvokeAction", invokeTimer));
        command.action.AddListener(() => controller.SetAllDisable(skillCooldown));
        command.Init(controller.commandController);
    }

    public void InvokeAction()
    {
        //controller.player.ResetState();
        controller.player.GetPlayerState().ChangeState(PlayerState.Idle);
    }
}
