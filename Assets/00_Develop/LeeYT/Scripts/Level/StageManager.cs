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

    private SoundController soundController;
    private void Update()
    {
        if (IsBoss)
        {
            GameManager.Instance.SetCameraRange(-6.35f, 10.55f);
        }
        if(WaveEnemyCount == 0)
            IsStopCamera = false;
    }
    public void NextStage(Vector3 pos, float minX, float maxX, GameObject cutScene = null, OptionUI data = null)
    {
        CurrentStage++;

        StartCoroutine(NextStageCou(0.5f, pos,minX, maxX, cutScene, data));
    }
    public void ActiveStage(MapNumber[] mapNumbers, int number, bool isActive)
    {
        foreach (MapNumber mapNumber in mapNumbers)
        {
            if (mapNumber.number == number)
            {
                mapNumber.gameObject.SetActive(isActive);
            }
            
        }
    }
    IEnumerator NextStageCou(float timer, Vector3 pos, float minX, float maxX, GameObject cutScene = null, OptionUI data = null)
    {
        GameManager.Instance.SetGameState(GameState.Stop);
        if (cutScene)
        {
            cutScene.SetActive(true);

        }
        else
        {
            UIManager.Instance.SetActiveFadeImage(true, 1, timer, fadeColor);
            
        }
        yield return new WaitForSeconds(timer + 0.5f);
        Utility.GetPlayerTr().position = pos;

        

        if (data != null)
        {
            CurrentStage = data.data.stageNum;
            ActiveStage(GameManager.Instance.mapNumbers, data.currentStage, false);
            ActiveStage(GameManager.Instance.mapNumbers, data.data.stageNum, true);
            if(data.data.option == OptionKind.Store)
                UIManager.Instance.ActiveStoreUI();

        }
        else
        {
            
            ActiveStage(GameManager.Instance.mapNumbers, CurrentStage - 1, false);
            ActiveStage(GameManager.Instance.mapNumbers, CurrentStage, true);
        }

        UIManager.Instance.bossHealthBar.gameObject.SetActive(false);
        GameManager.Instance.SetCameraRange(minX,maxX);
        if (cutScene)
            yield return new WaitUntil(() => cutScene.activeInHierarchy == false);
        else
            UIManager.Instance.SetActiveFadeImage(false, 0, timer, fadeColor);

        GameManager.Instance.SetGameState(GameState.Play);

    }

    public void PlayStageSound()
    {
        if (soundController == null)
            soundController = GetComponent<SoundController>();
        soundController.PlayLoopSound($"Stage_{CurrentStage}_BGM");
    }
}

