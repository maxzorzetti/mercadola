using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseListener<T, E, U> : MonoBehaviour, 
    Listener<T> where E : BaseEvent<T> where U : UnityEvent<T>
{
    public List<EventResponseTuple<E, U>> Events;
    
    public void OnEventRaised(BaseEvent<T> raisedEvent, T value)
    {
        Events.Find(x => x.Event == raisedEvent)?.Response?.Invoke(value);
    }
    
    void OnEnable()
    {
        foreach (var eventResponseTuple in Events)
        {
            if (eventResponseTuple.Event == null)
            {
                Debug.LogWarning($"GameObject '{name}' has a response set to a null event");
                continue;
            }
            
            eventResponseTuple.Event.RegisterListener(this);
        }
    }

    void OnDisable()
    {
        foreach (var eventResponseTuple in Events)
        {
            if (eventResponseTuple.Event == null) continue;
            
            eventResponseTuple.Event.DeregisterListener(this);
        }
    }
}
