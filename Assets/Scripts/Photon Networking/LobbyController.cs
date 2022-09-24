using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameEvent onJoinedRoom;

    public void StartButton()
    {
        JoinOrMakeRandomRoom();
    }

    public void CancelButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void JoinOrMakeRandomRoom()
    {
        string roomName = "Room" + Guid.NewGuid().ToString();
        RoomOptions roomOptions = new() { IsVisible = true, IsOpen = true, MaxPlayers = (byte) GameSettings.Instance.Players.Length };
        PhotonNetwork.JoinRandomOrCreateRoom(roomName: roomName, roomOptions: roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Create room... trying again");
        JoinOrMakeRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        onJoinedRoom.Raise();
    }
}
