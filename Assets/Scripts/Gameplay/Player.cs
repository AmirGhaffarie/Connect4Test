using Photon.Pun;
using System;
using UnityEngine;
[Serializable]
public class Player
{
    public string Name;
    public Color Color;
    [HideInInspector] public PhotonView PhotonView;

    public Player(string name, Color color, PhotonView photonView = null)
    {
        Name = name;
        Color = color;
        PhotonView = photonView;
    }
}
