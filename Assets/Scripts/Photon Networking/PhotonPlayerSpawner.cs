using Photon.Pun;
using System.IO;
using UnityEngine;

public class PhotonPlayerSpawner : MonoBehaviour
{
    private const string PREFABS_FOLDER_NAME = "Photon Prefabs";
    private const string PLAYER_PREFAB_NAME = "Photon Player";
    private void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine(PREFABS_FOLDER_NAME, PLAYER_PREFAB_NAME), Vector3.zero, Quaternion.identity);
    }

 
}
