using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum OptionKind { Stage, Store, Boss };
[System.Serializable]
public class OptionData 
{
    public OptionKind option;
    public string contents;
    public int stageNum;
    public GameObject cutScene;
}
