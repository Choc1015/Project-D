using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlayerController> players = new();
    private Dictionary<PlayerType, PlayerController> players_Dic = new();
    public EnemyStateMachine enemyTemp;
    public GameObject Camera;

    public float minX, maxX, minY, maxY;
    public Vector3 GetClampPosition(Transform T)
    {
        float x = Mathf.Clamp(T.position.x,Camera.transform.position.x - (17.85f /2f), Camera.transform.position.x + (17.85f / 2f));
        float y = Mathf.Clamp(T.position.y, minY, maxY);
        return (Vector3.right * x) + (Vector3.up * y);
    }
        

    private void Start()
    {
        foreach(PlayerController player in players)
        {
            players_Dic.Add(player.playerType, player);
        }
    }

    public void RevivePlayer(PlayerController curPlayer, PlayerType nextPlayer)
    {
        curPlayer.gameObject.SetActive(false);
        players_Dic[nextPlayer].transform.position = curPlayer.transform.position;
        players_Dic[nextPlayer].gameObject.SetActive(true);
    }
}
