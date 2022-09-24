using UnityEngine;

public class GameBoardSlot
{
    public Vector3 WorldPosition;
    public bool IsEmpty;
    public Player FillingPlayer;

    public GameBoardSlot(Vector3 worldPosition, bool isEmpty, Player fillingPlayer)
    {
        WorldPosition = worldPosition;
        IsEmpty = isEmpty;
        FillingPlayer = fillingPlayer;
    }
}
