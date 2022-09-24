using Photon.Pun;
using TMPro;
using UnityEngine;

public class JoinLogController : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI logTMP;
    [SerializeField] GameObject loadIndicator;

    private void Start()
    {
        SetLoadingLog("Connecting To Server");
    }

    public override void OnConnectedToMaster()
    {
        SetLog("Connected.");
    }

    public void StartedJoining()
    {
        SetLoadingLog("Joining Room");
    }

    public void StartedLeaving()
    {
        SetLoadingLog("Reconnecting to Server");
    }

    public override void OnJoinedRoom()
    {
        SetLoadingLog("Waiting For Opponent");
    }

    private void SetLoadingLog(string logText)
    {
        logTMP.text = logText;
        loadIndicator.SetActive(true);
    }
    private void SetLog(string logText)
    {
        logTMP.text = logText;
        loadIndicator.SetActive(false);
    }
}
