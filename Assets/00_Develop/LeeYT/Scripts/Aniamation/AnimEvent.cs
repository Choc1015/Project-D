using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public void PlayingShakeCamera_Slide()
    {
        Debug.Log("������ �̼���");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
    public void PlayingShakeCamera_Punch()
    {
        Debug.Log("������ �̼���");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
    public void PlayingShakeCamera_Breath()
    {
        Debug.Log("������ �̼���");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
}
