using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public SoundController SoundController;

    private bool isbreath;

    private void Start()
    {
        SoundController = GetComponent<SoundController>();
    }

    public void PlayingShakeCamera_Slide()
    {
        Debug.Log("������ �̼���");
        SoundController.PlayOneShotSound("slide");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
    public void PlayingShakeCamera_Punch()
    {
        Debug.Log("������ �̼���");
        SoundController.PlayOneShotSound("punch");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
    public void PlayingShakeCamera_Breath()
    {
        Debug.Log("������ �̼���");
        if (isbreath)
            StartCoroutine(breathSound());
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }

    IEnumerator breathSound()
    {
        isbreath = false;
        SoundController.PlayOneShotSound("breath");
        yield return new WaitForSeconds(5);
        isbreath = true;
    }
}
