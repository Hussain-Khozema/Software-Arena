using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex;

    //enable room join
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    //disable room join
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    //load UI for waiting room
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }

}
