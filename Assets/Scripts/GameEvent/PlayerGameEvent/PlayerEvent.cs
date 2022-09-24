using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Player Event")]
public class PlayerEvent : ScriptableObject
{
    private readonly List<IPlayerEventListener> listeners =
        new();

    public void Raise(Player player)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(player);
    }

    public void RegisterListener(IPlayerEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(IPlayerEventListener listener)
    { listeners.Remove(listener); }
}