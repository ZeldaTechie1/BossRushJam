using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}

public class GameEventListener : MonoBehaviour
{

    [Tooltip("Event to register with.")]
    public List<GameEvent> gameEvents;

    [Tooltip("Response to invoke when Event with GameData is raised.")]
    public List<UnityEvent> responses;

    private void OnEnable() {
        foreach(GameEvent gameEvent in gameEvents)
        {
            gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable() {
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(Component sender, object data, GameEvent gameEvent) {
        int gameEventIndex = gameEvents.IndexOf(gameEvent);
        responses[gameEventIndex].Invoke();
    }

}
