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
    public float skillManaValue;
    public bool isDisable;
    public void Init(SkillFunctionController controller)
    {
        this.controller = controller;
        
        command.action.AddListener(UseSkill);
        command.Init(controller.commandController);
    }

    public void InvokeAction()
    {
        controller.player.ResetState();
        //controller.player.GetPlayerState().ChangeState(PlayerState.Idle);
        controller.player.StopCommand();
        if (isDisable)
            gameObject.SetActive(false);
        if (skillObj)
            skillObj.SetActive(false);
    }
    public void UseSkill()
    {
        controller.player.GetPlayerState().ChangeState(PlayerState.Animation);
        Invoke("InvokeAction", invokeTimer);
        controller.SetAllDisable(skillCooldown);
        Utility.GetPlayer().GetStatController().GetStat(StatInfo.Mana).Value -= skillManaValue;
        Utility.GetPlayer().ActiveUpdatePlayerUI();
        if (skillObj)
            skillObj.SetActive(true);
    }
}
