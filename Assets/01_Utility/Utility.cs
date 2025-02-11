using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static PlayerController playerController;
    public static PlayerController GetPlayer() => playerController;
    public static Transform GetPlayerTr()
    {
        if (playerController == null)
            return null;
        return playerController.transform;
    }
    public static GameObject GetPlayerGO()
    {
        if (playerController == null)
            return null;

        return playerController?.gameObject;
    }
    public static void SetPlayer(GameObject player)
    {
        if (player)
            playerController = player.GetComponent<PlayerController>();
        else
        {
            playerController = null;
            playerStat = null;
        }
    }
    public static StatSO playerStat;
    public static StatSO GetPlayerStat() => playerStat;
    public static void SetStat(StatSO stat) => playerStat = stat;
    public static GameObject FindPlayers(ref GameObject Player)
    {

        if (!StageManager.Instance.IsStart)
            return null;

        GameObject[] Players;
        if ((Players = GameObject.FindGameObjectsWithTag("Player")) == null)
            return null;
        Debug.Log("플레이어 있음");
        int playerIndex = Random.Range(0, Players.Length);
        Player = Players[playerIndex];

        return Player;

    }
}
