using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="GameEvent")]
public class GameEvent : ScriptableObject
{

    public List<GameEventListener> listeners = new List<GameEventListener>();

    // Raise event through different method signatures
    // ############################################################

    public void Invoke() {
        Invoke(null, null);
    }

    public void Invoke(object data) {
        Invoke(null, data);
    }

    public void Invoke(Component sender) {
        Invoke(sender, null);
    }

    public void Invoke(Component sender, object data) {
        for (int i = listeners.Count -1; i >= 0; i--) {
            listeners[i].OnEventRaised(sender, data, this);
        }
    }

    // Manage Listeners
    // ############################################################

    public void RegisterListener(GameEventListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

}
