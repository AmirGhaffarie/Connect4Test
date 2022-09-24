using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    GameEvent gameEvent;
    private void OnEnable()
    {
        gameEvent = (GameEvent)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Raise"))
            gameEvent.Raise();
    }
}