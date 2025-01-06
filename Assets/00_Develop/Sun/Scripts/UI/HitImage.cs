using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitImage : UIBase
{
    public float disableTime;

    public void InvokeActiveGO(float timer)
    {
        CancelInvoke();
        Invoke("ActiveGO", timer);
        Invoke("DisableGO", timer + disableTime);
    }

}
