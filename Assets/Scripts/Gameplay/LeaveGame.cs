using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviour
{
    public void Leave()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene(GameSettings.Instance.JoinSceneIndex);
    }
}
