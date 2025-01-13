using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : EventTrigger
{
    public SoundData clip;
    public bool isLoop;
    public override void Enter()
    {
        if(isLoop)
            SoundManager.Instance.PlayLoopSound(clip);
        else
            SoundManager.Instance.PlayOneShotSound(clip);
    }
    public override void Execute(EventController eventController)
    {
        eventController.NextEvent();
    }
    public override void Exit()
    {
        
    }
}
