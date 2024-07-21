using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent<T> : ScriptableObject {

    List<EventListener<T>> listeners = new List<EventListener<T>>();

    public void Raise(T value)
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(EventListener<T> listener)
    {
        if (listeners.Contains(listener)) return;
        ;
        listeners.Add(listener);
    }

    public void DeregisterListener(EventListener<T> listener)
    {
        if (!listeners.Contains(listener)) return;
        
        listeners.Remove(listener);
    }
}