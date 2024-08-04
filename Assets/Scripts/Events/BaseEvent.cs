using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent<T> : ScriptableObject
{
    List<Listener<T>> listeners = new();

    public void Raise(T value)
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(this, value);
        }
    }

    public void RegisterListener(Listener<T> listener)
    {
        if (listeners.Contains(listener)) return;
        
        listeners.Add(listener);
    }

    public void DeregisterListener(Listener<T> listener)
    {
        if (!listeners.Contains(listener)) return;
        
        listeners.Remove(listener);
    }
}