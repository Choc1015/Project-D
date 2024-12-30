using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public GameObject Prefab; // 积己且 橇府普
    public int WaveCount; // 傀捞宏 墨款飘

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
            Debug.Log("利 积己");
        }
        // 利 积己
    }
}
