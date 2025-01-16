using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePoint : MonoBehaviour
{
    [SerializeField] private Vector3 waveCheck;
    public int waveCount = 1;
    public bool isWave = false;

    [SerializeField] private GameObject Player;
    public GameObject cutScene;

    void OnEnable()
    {
        isWave = false;
    }

    private void Update()
    {
        if (!StageManager.Instance.IsStart || isWave)
            return;

        // �÷��̾�� �� ������ ���� �� �׸���
        if (Utility.GetPlayerGO() != null)
        {
            // �÷��̾ ���� ���� ���� �� �ʷϻ� ��
            if ((transform.position.x - Utility.GetPlayerTr().position.x) <= (-waveCheck.x / 2f + 2f))
            {
                Debug.Log("Start");
                WaveManager.WaveStart(waveCount);
                if (StageManager.Instance.WaveEnemyCount != 0)
                {
                    StageManager.Instance.SetCameraStop(true, this);
                    
                }
                else if(cutScene)
                    cutScene.SetActive(true);
                
                
                isWave = true;
            }

        }
        //else
        //{

        //    //Utility.FindPlayers(ref Player);
        //    Player = Utility.GetPlayer().gameObject;
        //}
    }

    private void OnDrawGizmos()
    {
    // ���� ���� �ð�ȭ
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(transform.position, waveCheck);

    //    if (!StageManager.Instance.IsStart)
    //        return;

    //        // �÷��̾�� �� ������ ���� �� �׸���
    //        if (Player != null)
    //        {
    //            // �÷��̾ ���� ���� ���� �� �ʷϻ� ��
    //            if ((transform.position.x - Player.transform.position.x) <= (-waveCheck.x / 2f + 2f))
    //            {
    //                Gizmos.color = Color.green;
    //                Gizmos.DrawWireCube(transform.position, waveCheck);
    //                WaveManager.WaveStart(waveCount);
    //                if (StageManager.Instance.WaveEnemyCount != 0)
    //                    StageManager.Instance.IsStopCamera = true;
    //                isWave = true;
    //            }

    //        }
    //        else
    //        {

    //            Utility.FindPlayers(ref Player);
    //        }
    //}
    }

}
