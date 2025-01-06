using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.core;

public class CameraShake : MonoBehaviour
{
    public Cinemachine.CinemachineImpulseSource cinemachine;
    //public AnimationCurve shakeCurve;
    private void Start()
    {
        cinemachine = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }
    public void ActiveCameraShake()
    {
        cinemachine.m_DefaultVelocity = RandomVec(-0.1f, 0.1f);
        cinemachine.GenerateImpulse();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ActiveCameraShake();
    }

    private Vector3 RandomVec(float min, float max)
    {
        Vector3 vec = Vector3.zero;
        vec.x = Random.Range(min, max);
        vec.y = Random.Range(min, max);
        return vec;
    }

}
