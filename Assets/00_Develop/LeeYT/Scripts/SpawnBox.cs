using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public GameObject Prefab; // ������ ������
    public int WaveCount; // ���̺� ī��Ʈ

    private void OnEnable()
    {
        WaveManager.OnWaveStart += SpawnEnemy;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveStart -= SpawnEnemy;
    }
    private void Start()
    {

    }

    private void SpawnEnemy(int wavePoint)
    {

        if (wavePoint == WaveCount)
        {
            GameObject enemy = ObjectPoolManager.Instance.SpawnFromPool(Prefab.name, transform.position);
            gameObject.SetActive(false);
            StageManager.Instance.WaveEnemyCount++;
            Debug.Log($"�� ���� ���� ��ȣ{wavePoint} , ���̺� ���� ��ȣ");
        }


        // �� ����
    }
}
