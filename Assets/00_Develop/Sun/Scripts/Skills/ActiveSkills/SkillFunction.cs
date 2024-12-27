using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class SkillFunction : MonoBehaviour
{
    public SkillCommand command;
    public GameObject skillObj;
    public void Init(SkillCommandController controller)
    {
        // 상태 변환하는 함수
        //command.action.AddListener();
        // 인보크하는 함수
        command.Init(controller);
    }
}
