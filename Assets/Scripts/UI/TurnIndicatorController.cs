using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicatorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image backdropImage;
    [SerializeField] private CanvasGroupAlphaAnimator canvasGroupAlphaAnimator;
    public void OnTurnChanged(Player player)
    {
        if (player.PhotonView.IsMine)
            canvasGroupAlphaAnimator.FadeIn();
        else if(canvasGroupAlphaAnimator.IsVisible)
            canvasGroupAlphaAnimator.FadeOut();
    }

    public void OnLocalPlayerJoin(Player localplayer)
    {
        backdropImage.color = localplayer.Color;
    }
}
