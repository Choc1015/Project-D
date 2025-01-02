using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType { Bool, Trigger };
public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public void TriggerAnim(string triggerName, AnimationType animType, bool isBool = false)
    {
        if (animType == AnimationType.Bool)
            anim.SetBool(triggerName, isBool);
        else
            anim.SetTrigger(triggerName);
    }
    public void SetIntegerAnim(string triggerName, int num)
    {
        anim.SetInteger(triggerName, num);
    }
}
