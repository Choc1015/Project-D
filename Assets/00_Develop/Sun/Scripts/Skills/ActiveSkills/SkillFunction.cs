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
        // ���� ��ȯ�ϴ� �Լ�
        //command.action.AddListener();
        // �κ�ũ�ϴ� �Լ�
        command.Init(controller);
    }
}
