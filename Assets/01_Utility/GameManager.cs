using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { Play, Stop }
public class GameManager : Singleton<GameManager>
{
    public PlayerController[] playerPrefabs;
    private Dictionary<PlayerType, PlayerController> players_Dic = new();
    public GameObject Camera;

    public float minX, maxX, minY, maxY;
    [HideInInspector] public float maxXTemp;

    public GameState currentState { get; private set; }
    public void SetGameState(GameState gameState) => currentState = gameState;

    public Action SetLayerPosition;
    public MapNumber[] mapNumbers;
    public Vector3 GetClampPosition(Transform T)
    {
        float x = 0;
        if (StageManager.Instance.IsStopCamera)
            x = Mathf.Clamp(T.position.x, Camera.transform.position.x - (17.85f / 2f), Camera.transform.position.x + (17.85f / 2f));
        else
        {
            if(maxXTemp != 0)
                x = Mathf.Clamp(T.position.x, minX, maxXTemp);
            else
                x = Mathf.Clamp(T.position.x, minX, maxX);
        }

        float y = Mathf.Clamp(T.position.y, minY, maxY);
        return (Vector3.right * x) + (Vector3.up * y) + (Vector3.forward * T.position.z) ;
    }

    private void Update()
    {
        SetLayerPosition?.Invoke();
    }
    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        base.Awake();
    }
    protected override void Start()
    {
        playerPrefabs[0] = Resources.Load<PlayerController>("Prefabs/Player/Player_Warrior");
        playerPrefabs[1] = Resources.Load<PlayerController>("Prefabs/Player/Player_Priest");
        playerPrefabs[2] = Resources.Load<PlayerController>("Prefabs/Player/Player_Wizard");

        foreach (PlayerController playerPrefab in playerPrefabs)
        {
            PlayerController player = Instantiate(playerPrefab);
            players_Dic?.Add(player.playerType, player);

        }

        players_Dic[PlayerType.Warrior].gameObject.SetActive(true);
        mapNumbers = Resources.FindObjectsOfTypeAll<MapNumber>();

    }

    public void RevivePlayer(PlayerController curPlayer, ReviveInfo info)
    {
        curPlayer.gameObject.SetActive(false);
        players_Dic[info.nextPlayer].transform.position = curPlayer.transform.position;
        players_Dic[info.nextPlayer].gameObject.SetActive(true);
        info.statue.gameObject.SetActive(false);
    }
    public void SetCameraRange(float newMinX, float newMaxX)
    {
        minX = newMinX;
        maxX = newMaxX;
    }
}
