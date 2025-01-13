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

    [Header("Options")]
    public bool isOption;
    public OptionData[] options;

    public bool isBossStage;

    private void Start()
    {
        //StageManager.Instance.IsBoss = isBossStage;
    }
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
                if(isOption)
                    ActiveOption();
                else
                    GoNextStage();

            }

        }
        //else
        //{

        //    //Utility.FindPlayers(ref Player);
        //    Player = Utility.GetPlayer().gameObject;
        //}
    }

    public void GoNextStage(OptionUI option = null, GameObject cutScene = null)
    {
        StageManager.Instance.NextStage(nextStagePos, minX, maxX, cutScene, option);
    }
    public void ActiveOption()
    {
        UIManager.Instance.optionController.Init(options, this, curStage);
        //StageManager.Instance.ActiveStage(GameManager.Instance.mapNumbers, curStage, false);
    }
}
