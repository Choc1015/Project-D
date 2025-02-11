using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering;

public class LobbyManager : MonoBehaviourPunCallbacks // 로비 제작 전에 사용하는 임시 코드
{
    
    public bool isJoin;
    public string roomName;
    void Awake()
    {
        PhotonNetwork.SendRate = 500;
        PhotonNetwork.SerializationRate = 500;
        Debug.Log(" started");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!");
        PhotonNetwork.JoinLobby(); // 로비에 참여
    }

    // 로비에 참여 완료 시 호출
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!");
        if (isJoin)
            JoinRoom(roomName);
        else
            CreateRoom(roomName); // 방 생성
    }
    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
