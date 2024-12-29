using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public GameObject Prefab; // 积己且 橇府普

    private void Start()
    {
        // 利 积己
        var enemy = ObjectPoolManager.Instance.SpawnFromPool(Prefab.name, transform.position);
        gameObject.SetActive(false);
        Debug.Log("利 积己");

    }

}
