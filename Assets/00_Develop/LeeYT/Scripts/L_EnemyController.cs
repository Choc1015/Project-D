using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class L_EnemyController : Human
{
    private GameObject Player;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        statController.Init();
        FindPlayers();
    }

    private void FindPlayers()
    {
        GameObject[] Players;
        if ( (Players = GameObject.FindGameObjectsWithTag("Player")) == null)
                return;

        int playerIndex = Random.Range(0, Players.Length);
        Player = Players[playerIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) 
        FollowPlayer();
    }

    void FollowPlayer()
    {
        moveDir = Player.transform.position - transform.position;

        if(moveDir != null)
        movement.MoveToRigid(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
    }


}
