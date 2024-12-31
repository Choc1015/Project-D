using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePoint : MonoBehaviour
{
    [SerializeField] private Vector3 waveCheck;
    public int waveCount = 1;
    public bool isWave = false;

    private GameObject Player;

    private void Start()
    {
        FindPlayers();
    }


    private void OnDrawGizmos()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, waveCheck);



        // �÷��̾�� �� ������ ���� �� �׸���
        if (Player != null)
        {
            // �÷��̾ ���� ���� ���� �� �ʷϻ� ��
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= waveCheck.x / 2f)
            {
                Debug.Log("�÷��̾ ���� ���� ���� �� �ʷϻ� ��");
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position, waveCheck);
                WaveManager.WaveStart(waveCount);
                isWave = true;
            }

        }
    }
    private void FindPlayers()
    {
        GameObject[] Players;
        if ((Players = GameObject.FindGameObjectsWithTag("Player")) == null)
            return;

        int playerIndex = Random.Range(0, Players.Length);
        Player = Players[playerIndex];
    }

}
