using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject CancelButton;
    [SerializeField]
    private int RoomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        StartButton.SetActive(true); // player can only go to waiting room when they connected to server
    }

    //goes to waiting room
    public void DelayStart()
    {
        StartButton.SetActive(false);
        CancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Delay start");
    }

    //if there is no room in server, creates a room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    //creates a room in server
    void CreateRoom()
    {
        Debug.Log("Creating room now");
        RoomOptions options = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = (byte)RoomSize};
        PhotonNetwork.CreateRoom("Room " + UnityEngine.Random.Range(1, 1000) ,options);
    }

    //create room if fail to create room
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }


    //leave waiting room
    public void DelayCancel()
    {
        CancelButton.SetActive(false);
        StartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
