using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public int CurrentStage { get; private set; }
    public int WaveEnemyCount;
    public bool IsBoss = false;
    public bool IsStopCamera= false;
    public bool IsStart = false;
    public Color fadeColor;
    private void Update()
    {
        if(WaveEnemyCount == 0)
            IsStopCamera = false;
    }
    public void NextStage(Vector3 pos, float minX, float maxX, GameObject cutScene = null)
    {
        CurrentStage++;
        StartCoroutine(NextStageCou(0.5f, pos,minX, maxX));
    }
    IEnumerator NextStageCou(float timer, Vector3 pos, float minX, float maxX, GameObject cutScene = null)
    {
        UIManager.Instance.SetActiveFadeImage(true, 1, timer, fadeColor);
        yield return new WaitForSeconds(timer+0.5f);
        Utility.GetPlayerTr().position = pos;
        GameManager.Instance.SetCameraRange(minX,maxX);
        UIManager.Instance.SetActiveFadeImage(false, 0, timer, fadeColor);
        if (cutScene)
            cutScene.SetActive(true);
    }
}
