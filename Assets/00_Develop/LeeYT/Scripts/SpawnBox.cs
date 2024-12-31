using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public GameObject Prefab; // 생성할 프리팹
    public int WaveCount; // 웨이브 카운트

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
            Debug.Log($"적 생성 스폰 번호{wavePoint} , 웨이브 구역 번호");
        }


        // 적 생성
    }
}
