using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class OptionController : MonoBehaviour
{
    public OptionUI[] optionDatas;
    public void Init(OptionData[] options, NextStage nextStage,  int currentStage)
    {
        gameObject.SetActive(true);
        for(int i=0; i<options.Length; i++)
        {
            optionDatas[i].Init(options[i], nextStage, currentStage);
        }
        GameManager.Instance.SetGameState(GameState.Stop);
    }

    public void ChoiceOption()
    {
        foreach (OptionUI option in optionDatas)
        {
            option.DisableGO();
        }
        gameObject.SetActive(false);
    }
}
