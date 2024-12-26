using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFunctionController : MonoBehaviour
{
    public SkillFunction[] skillFunctions;
    public SkillCommandController commandController;
    private int currentSkillIndex;
    
    public void ChangeSkill()
    {
        currentSkillIndex++;
        if (currentSkillIndex >= skillFunctions.Length)
            currentSkillIndex = 0;

        commandController.skillCommands[0] = skillFunctions[currentSkillIndex].command;

    }
}
