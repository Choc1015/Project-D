using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlayerController> players = new();
    public EnemyStateMachine enemyTemp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            players[0].TakeDamage(5, enemyTemp, new KnockBackInfo(Vector3.zero, 100, 0.3f, 0.5f));
        }
    }
}
