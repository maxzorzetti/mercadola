using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseListener<T, E, U> : MonoBehaviour, 
    EventListener<T> where E : BaseEvent<T> where U : UnityEvent<T>
{
    public E Event;
    public U Response;
    
    public void OnEventRaised(T value)
    {
        // Events.Find(x => x.Event == raisedEvent)?.Response?.Invoke(value);
        Response?.Invoke(value);
    }
    
    void OnEnable()
    {
        Event.RegisterListener(this);
        // foreach (var eventResponseTuple in Events)
        // {
        //     eventResponseTuple.Event.RegisterListener(this);
        // }
    }

    void OnDisable()
    {
        Event.DeregisterListener(this);
        // foreach (var eventResponseTuple in Events)
        // {
        //     eventResponseTuple.Event.DeregisterListener(this);
        // }
    }
    
    [Serializable]
    public class EventResponseTuple<E, U>
    {
        public E Event;
        public U Response;
    }
}