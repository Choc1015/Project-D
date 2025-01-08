using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventTrigger : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Execute(EventController eventController);
    public abstract void Exit();
}
