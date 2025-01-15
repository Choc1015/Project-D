using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillFunctionController : MonoBehaviour
{
    public PlayerController player;
    public SkillFunction[] skillFunctions;
    public SkillCommandController commandController;
    private int currentSkillIndex;
    private bool[] isInit;
    private bool isAllDisable;
    void Start()
    {
        isInit = new bool[skillFunctions.Length];
       
        currentSkillIndex = -1;
        ChangeSkill();
    }

    public void ChangeSkill()
    {
        if(currentSkillIndex >= 0)
        {
            skillFunctions[currentSkillIndex].command.isDisable = true;
        }
        currentSkillIndex++;
        if (currentSkillIndex >= skillFunctions.Length)
            currentSkillIndex = 0;

        commandController.skillCommands[0] = skillFunctions[currentSkillIndex].command;

        if(!isAllDisable)
            skillFunctions[currentSkillIndex].command.isDisable = false;

        if (!isInit[currentSkillIndex])
        {
            skillFunctions[currentSkillIndex].Init(this);
            isInit[currentSkillIndex] = true;
        }
    }
    private void Update()
    {
        if(!isAllDisable)
        {
            skillFunctions[currentSkillIndex].command.isDisable =
            skillFunctions[currentSkillIndex].skillManaValue > Utility.GetPlayer().GetStatController().GetStat(StatInfo.Mana).Value;
        }
            
    }
    public void SetAllDisable(float resetTime = 5)
    {
        if (resetTime == 0)
            return;
        isAllDisable = true;
        foreach (SkillFunction skillFunction in skillFunctions)
        {
            skillFunction.command.isDisable = true;
        }
        if(resetTime < 20)
            Invoke("ResetValue", resetTime);
    }
    public void ResetValue()
    {
        isAllDisable = false;
        skillFunctions[currentSkillIndex].command.isDisable = false;
    }
}
