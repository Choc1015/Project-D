using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public EnemyStateMachine Prefab = new(); // ������ ������

    private void Start()
    {
        // �� ����
        var enemy = EnemyManager.Instance.SpawnEnemy(Prefab, transform.position);
        gameObject.SetActive(false);
        Debug.Log("�� ����");

    }

}
