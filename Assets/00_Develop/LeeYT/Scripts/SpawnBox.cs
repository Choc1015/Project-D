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
        WaveManager.OnWaveStart += WaveStart;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveStart -= WaveStart;
    }
    private void Start()
    {

    }

    private void WaveStart(int wavePoint)
    {
        if(wavePoint == WavePoint.Instance.waveCount && gameObject.activeSelf)
        {
            var enemy = ObjectPoolManager.Instance.SpawnFromPool(Prefab.name, transform.position);
            gameObject.SetActive(false);
            Debug.Log("�� ����");
        }
        // �� ����
    }
}
