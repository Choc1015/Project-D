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
        Debug.Log("흔들려라 이세상");
        SoundController.PlayOneShotSound("slide");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
    public void PlayingShakeCamera_Punch()
    {
        Debug.Log("흔들려라 이세상");
        SoundController.PlayOneShotSound("punch");
        CameraShake.cameraShake.ActiveCameraShake(0.2f);
    }
    public void PlayingShakeCamera_Breath()
    {
        Debug.Log("흔들려라 이세상");
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
