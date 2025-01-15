using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    [SerializeField] private Vector3 stageCheck;
    [SerializeField] private Vector3 nextStagePos;
    public int curStage;
    [SerializeField] private GameObject Player;
    [SerializeField] private float minX, maxX;
    [SerializeField] private float minY, maxY;
    public GameObject cutScene;

    [Header("Options")]
    public bool isOption;
    public OptionData[] options;

    public bool isBossStage;
    public BossBase boss;
    public MagicVilan magicVilan;
    public DragonBoss dragon;

    private bool isDisable;
    private void Start()
    {
        //StageManager.Instance.IsBoss = isBossStage;
    }
    void Update()
    {
        if (!StageManager.Instance.IsStart || isDisable)
            return;

        // 플레이어와 적 사이의 연결 선 그리기
        if (Utility.GetPlayerGO() != null)
        {
            // 플레이어가 범위 내에 있을 때 초록색 선
            if (isBossStage&& curStage == StageManager.Instance.CurrentStage)
            {
                if (boss)
                    EndBossStage(boss.GetAlive());
                else if (magicVilan && !magicVilan.GetAlive())
                    GoNextStage(null, cutScene, true);
                //else if(!dragon.GetAlive())
                    
            }
            else
            {
                if ((transform.position.x - Utility.GetPlayerTr().position.x) <= (-stageCheck.x / 2f + 2f) && curStage == StageManager.Instance.CurrentStage)
                {
                    if (isOption)
                        ActiveOption();
                    else
                        GoNextStage();
                }
            }

        }
        
        //else
        //{

        //    //Utility.FindPlayers(ref Player);
        //    Player = Utility.GetPlayer().gameObject;
        //}
    }

    public void GoNextStage(OptionUI option = null, GameObject cutScene = null, bool isDragon = false)
    {
        StageManager.Instance.NextStage(nextStagePos, minX, maxX, minY, maxY,cutScene, option, isDragon);
    }
    public void ActionGoNextStage() => GoNextStage();
    public void ActiveOption()
    {
        UIManager.Instance.optionController.Init(options, this, curStage);
        //StageManager.Instance.ActiveStage(GameManager.Instance.mapNumbers, curStage, false);
    }

    public void EndBossStage(bool isAlive)
    {
        if (!isAlive)
        {
            cutScene?.SetActive(true);
            cutScene = null;
            isDisable = true;
        }
    }
}
