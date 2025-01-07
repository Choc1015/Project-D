using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public int CurrentStage { get; private set; }
    public int WaveEnemyCount;
    public bool IsStopCamera= false;
    public bool IsStart = false;

    private void Update()
    {
        if(WaveEnemyCount == 0)
            IsStopCamera = false;
    }
    public void NextStage(Vector3 pos)
    {
        CurrentStage++;
        StartCoroutine(NextStageCou(0.5f, pos));
    }
    IEnumerator NextStageCou(float timer, Vector3 pos)
    {
        UIManager.Instance.SetActiveFadeImage(true, 1, timer);
        yield return new WaitForSeconds(timer+0.5f);
        Utility.GetPlayerTr().position = pos;
        UIManager.Instance.SetActiveFadeImage(true, 0, timer);
    }
}
