using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBoss : MonoBehaviour
{
    public GameObject boss;

    void Start()
    {
        Invoke("Delay", 1.5f);
    }

    void Delay()
    {
        boss.SetActive(true);
    }

    void Update()
    {
        
    }
}
