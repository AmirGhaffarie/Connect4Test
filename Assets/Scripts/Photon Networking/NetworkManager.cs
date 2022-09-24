using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameEvent connectedToMaster;
    private void Start()
    {
        if (PhotonNetwork.CurrentRoom != null)
            PhotonNetwork.LeaveRoom();
        else if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
        else
            connectedToMaster.Raise();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected to {PhotonNetwork.CloudRegion} Server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        connectedToMaster.Raise();
    }
}
