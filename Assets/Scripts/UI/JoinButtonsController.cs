using UnityEngine;
using UnityEngine.UI;

public class JoinButtonsController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button cancelButton;

    public void StartButton()
    {
        startButton.interactable = false;
        cancelButton.interactable = false;
        startButton.gameObject.SetActive(false);

        cancelButton.gameObject.SetActive(true);

    }

    public void CancelButton()
    {
        startButton.interactable = false;
        cancelButton.interactable = false;
        cancelButton.gameObject.SetActive(false);

        startButton.gameObject.SetActive(true);
    }

    public void OnJoinedRoom()
    {
        cancelButton.interactable = true;
    }

    public void OnConnectedToMaster()
    {
        startButton.interactable = true;
    }
}
