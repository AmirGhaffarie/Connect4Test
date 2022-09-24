using UnityEngine;
using UnityEngine.Events;

public class PlayerEventListener : MonoBehaviour, IPlayerEventListener
{
    public PlayerEvent Event;
    public UnityEvent<Player> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(Player player)
    {
        Response.Invoke(player);
    }
}
