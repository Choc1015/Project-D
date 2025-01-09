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
    public GameObject cutScene;
    
    void Update()
    {
        if (!StageManager.Instance.IsStart)
            return;

        // 플레이어와 적 사이의 연결 선 그리기
        if (Utility.GetPlayerGO() != null)
        {
            // 플레이어가 범위 내에 있을 때 초록색 선
            if ((transform.position.x - Utility.GetPlayerTr().position.x) <= (-stageCheck.x / 2f + 2f) && curStage == StageManager.Instance.CurrentStage)
            {

                StageManager.Instance.NextStage(nextStagePos, minX, maxX, cutScene);
            }

        }
        //else
        //{

        //    //Utility.FindPlayers(ref Player);
        //    Player = Utility.GetPlayer().gameObject;
        //}
    }
}
