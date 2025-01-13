using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapNumber : MonoBehaviour
{
    public int number;
    public bool isBossStage;
    private void Start()
    {
        StageManager.Instance.IsBoss = isBossStage;
    }

    private void OnEnable()
    {
        foreach(Transform children in transform)
        {
            children.gameObject.SetActive(true);
        }
    }
}
