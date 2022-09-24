using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using UnityEngine;

public class PhotonPlayerController : MonoBehaviourPun
{
    private void Start()
    {
        photonView.RPC(nameof(AssignPlayer), RpcTarget.All);
    }

    [PunRPC]
    private void AssignPlayer()
    {
        PhotonPlayerSpawner photonPlayerSpawner = FindObjectOfType<PhotonPlayerSpawner>();
        transform.SetParent(photonPlayerSpawner.transform);
        StartCoroutine(SetPlayerOnGameManager());
    }

    private IEnumerator SetPlayerOnGameManager()
    {
        yield return new WaitWhile(()=> GetPlayerIndex() == -1);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.AddPlayer(photonView, GetPlayerIndex());
    }

    private int GetPlayerIndex()
    {
        return photonView.Owner.GetPlayerNumber();
    }
}
