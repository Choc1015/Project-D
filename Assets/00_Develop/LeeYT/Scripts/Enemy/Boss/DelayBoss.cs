using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBoss : MonoBehaviour
{
    public bool isBossScene = true;
    public GameObject boss;
    public float delayTime;

    void Start()
    {
        if(isBossScene)
            Invoke("Delay", delayTime);
    }

    void Delay()
    {
        if(isBossScene)
            boss.SetActive(true);
        else
            boss.GetComponent<BossBase>().enabled = true;
    }

    void Update()
    {
        
    }
}
