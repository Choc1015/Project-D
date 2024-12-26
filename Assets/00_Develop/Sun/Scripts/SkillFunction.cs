using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class SkillFunction : MonoBehaviour
{
    public SkillCommand command;
    public GameObject skillObj;

    public void ActiveObj()
    {
        gameObject.SetActive(true);
    }
    public void DisableObj()
    {
        gameObject.SetActive(false);
    }
}
