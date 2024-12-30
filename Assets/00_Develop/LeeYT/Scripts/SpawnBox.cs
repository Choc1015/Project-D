using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public GameObject Prefab; // ������ ������

    private void Start()
    {
        // �� ����
        var enemy = ObjectPoolManager.Instance.SpawnFromPool(Prefab.name, transform.position);
        gameObject.SetActive(false);
        Debug.Log("�� ����");

    }

}
