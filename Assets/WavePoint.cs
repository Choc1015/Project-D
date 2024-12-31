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
        // 공격 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, waveCheck);



        // 플레이어와 적 사이의 연결 선 그리기
        if (Player != null)
        {
            // 플레이어가 범위 내에 있을 때 초록색 선
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= waveCheck.x / 2f)
            {
                Debug.Log("플레이어가 범위 내에 있을 때 초록색 선");
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
