using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class IngameLogController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI logTMP;
    [SerializeField] private GameObject loadIndicator;


    private void Start()
    {
        SetLoadingLog("Waiting For Opponent Load");
    }

    public void OnGameStart()
    {
        CloseLog();
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        SetLog("Your Opponent Left.");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SetLog("You got disconnected.");
    }
    private void CloseLog()
    {
        logTMP.text = string.Empty;
        loadIndicator.SetActive(false);
        panel.SetActive(false);
    }

    private void SetLoadingLog(string logText)
    {
        logTMP.text = logText;
        loadIndicator.SetActive(true);
        panel.SetActive(true);
    }
    private void SetLog(string logText)
    {
        logTMP.text = logText;
        loadIndicator.SetActive(false);
        panel.SetActive(true);
    }
}
