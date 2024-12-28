using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlayerController> players = new();
    public EnemyStateMachine enemyTemp;

    void Update()
    {

    }
}
