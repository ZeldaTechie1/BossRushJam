using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    [Tooltip("Event to register with.")]
    public List<CustomGameEvent> gameEvents;

    [Tooltip("Response to invoke when Event with GameData is raised.")]

    private void OnEnable() {
        for(int count = 0; count < gameEvents.Count; count++)
        {
            gameEvents[count].gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable() {
        for (int count = 0; count < gameEvents.Count; count++)
        {
            gameEvents[count].gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(Component sender, object data, GameEvent gameEvent) {
        int gameEventIndex = -1;
        for(int count = 0; count < gameEvents.Count; count++)
        {
            if (gameEvents[count].gameEvent == gameEvent)
            {
                gameEventIndex = count;
                break;
            }
        }
        if(gameEventIndex == -1)
        {
            Debug.LogWarning("Could not find event!");
            return;
        }
        gameEvents[gameEventIndex].response.Invoke(sender,data);
    }

}

[Serializable]
public class CustomGameEvent
{
    public GameEvent gameEvent;
    public UnityEvent<Component, object> response;
}
