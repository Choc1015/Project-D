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
    void Update()
    {
        if (!StageManager.Instance.IsStart)
            return;

        // �÷��̾�� �� ������ ���� �� �׸���
        if (Player != null)
        {
            // �÷��̾ ���� ���� ���� �� �ʷϻ� ��
            if ((transform.position.x - Player.transform.position.x) <= (-stageCheck.x / 2f + 2f) && curStage == StageManager.Instance.CurrentStage)
            {

                StageManager.Instance.NextStage(nextStagePos);
            }

        }
        else
        {

            //Utility.FindPlayers(ref Player);
            Player = Utility.GetPlayer().gameObject;
        }
    }
}
