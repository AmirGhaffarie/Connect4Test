using Photon.Pun;
using TMPro;
using UnityEngine;

public class EndGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject leaveButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject notify;
    [SerializeField] TextMeshProUGUI winTextMesh;
    public void OnPlayerWin(Player player)
    {
        SetText(player);
        ShowButtons();
    }

    private void ShowButtons()
    {
        leaveButton.SetActive(true);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            restartButton.SetActive(true);
        }
        else
        {
            notify.SetActive(true);
        }
    }

    private void SetText(Player player)
    {
        if(player == null)
        {
            winTextMesh.text = "It's a Tie.";
            winTextMesh.color = Color.yellow;
        }
        else
        {
            if (player.PhotonView.IsMine)
            {
                winTextMesh.text = "You Won!";
            }
            else
            {
                winTextMesh.text = "You Lost...";
            }
            winTextMesh.color = player.Color;
        }
        winTextMesh.enabled = true;
    }
}
