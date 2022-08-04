using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string name;
    public int sceneIndex;
    public int maxPlayer;
}

public class PUNNetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject roomUI;
    KeyBoard keyboard;

    void Start()
    {

    }

    // Update is called once per frame
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying to Connect to Server...");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined A Lobby");
        roomUI.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];
        Debug.Log("1");

        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
        Debug.Log("2");
        RoomOptions roomOptions = new RoomOptions();
        Debug.Log("3");
        roomOptions.MaxPlayers = (byte) roomSettings.maxPlayer;
        Debug.Log("4");
        roomOptions.IsVisible = true;
        Debug.Log("5");
        roomOptions.IsOpen = true;
        Debug.Log("6");

        PhotonNetwork.JoinOrCreateRoom(roomSettings.name, roomOptions, TypedLobby.Default);
        Debug.Log("7");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room Created");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined A Room");
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player joined the room");
    }
}
