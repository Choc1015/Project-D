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

        // �÷��̾�� �� ������ ���� �� �׸���
        if (Utility.GetPlayerGO() != null)
        {
            // �÷��̾ ���� ���� ���� �� �ʷϻ� ��
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
