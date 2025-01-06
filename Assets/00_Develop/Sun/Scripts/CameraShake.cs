using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.core;

public class CameraShake : MonoBehaviour
{
    public static CameraShake cameraShake; 
    private Cinemachine.CinemachineImpulseSource cinemachine;
    //public AnimationCurve shakeCurve;
    private void Start()
    {
        cameraShake = this;
        cinemachine = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }
    public void ActiveCameraShake(float power)
    {
        cinemachine.m_DefaultVelocity = RandomVec(-power, power);
        cinemachine.GenerateImpulse();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ActiveCameraShake(0.1f);
    }

    private Vector3 RandomVec(float min, float max)
    {
        Vector3 vec = Vector3.zero;
        vec.x = Random.Range(min, max);
        vec.y = Random.Range(min, max);
        return vec;
    }

}
