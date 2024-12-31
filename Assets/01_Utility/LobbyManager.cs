using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public bool isJoin;
    void Awake()
    {
        Debug.Log(" started");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!");
        PhotonNetwork.JoinLobby(); // �κ� ����
    }

    // �κ� ���� �Ϸ� �� ȣ��
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!");
        if (isJoin)
            JoinRoom("TestRoom");
        else
            PhotonNetwork.CreateRoom("TestRoom"); // �� ����
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
