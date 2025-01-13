using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBoss : MonoBehaviour
{
    public GameObject boss;
    public float delayTime;

    void Start()
    {
        //Invoke("Delay", delayTime);
    }

    void Delay()
    {
        //boss.SetActive(true);
        boss.GetComponent<BossBase>().enabled = true;
    }

    void Update()
    {
        
    }
}
