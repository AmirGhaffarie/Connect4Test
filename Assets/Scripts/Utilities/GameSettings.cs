using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Settings")]
public class GameSettings : ScriptableObject
{
    private const string PATH = "GameSettings";
    #region Singleton
    static GameSettings instance;
    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameSettings>(PATH);
                if (instance == null)
                    Debug.LogError($"File {PATH} does not exist in resources.");
            }

            return instance;
        }
    }
    #endregion
    [Header("Settings")]
    public Player[] Players;
    public int ConnectionsNeededForWin = 4;
    [Header("Scene Indexes")] 
    public int JoinSceneIndex = 0;
    public int GameSceneIndex = 1;

}
