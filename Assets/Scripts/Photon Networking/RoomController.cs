using Photon.Pun;
using System.Collections;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        StartGame();
    }

    private void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(WaitForStartingGames());
            Debug.Log("Waiting to Start Game");
        }
    }

    private bool StillInRoom => PhotonNetwork.CurrentRoom != null;

    IEnumerator WaitForStartingGames()
    {
        WaitForSeconds waitFor100ms = new(.1f);
        while (StillInRoom && PhotonNetwork.CurrentRoom.PlayerCount < 2)
            yield return waitFor100ms;
        if (StillInRoom)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
            PhotonNetwork.LoadLevel(GameSettings.Instance.GameSceneIndex);
        }
    }
}
