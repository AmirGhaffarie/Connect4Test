using Photon.Pun;

public class RestartGame : MonoBehaviourPun
{
    public void Restart()
    {
        photonView.RPC(nameof(RestartScene),RpcTarget.All);
    }

    [PunRPC]
    private void RestartScene()
    {
        PhotonNetwork.LoadLevel(GameSettings.Instance.GameSceneIndex);
    }
}
