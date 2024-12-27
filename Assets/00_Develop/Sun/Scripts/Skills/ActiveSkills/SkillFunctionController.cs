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
        skillFunctions[currentSkillIndex].command.isDisable = false;

        if (!isInit[currentSkillIndex])
        {
            skillFunctions[currentSkillIndex].Init(this);
            isInit[currentSkillIndex] = true;
        }
    }
}
