using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Listener : MonoBehaviour
{
    public List<EventResponseTuple> Events;

    void OnEnable()
    {
        foreach (var eventResponseTuple in Events)
        {
            eventResponseTuple.Event.RegisterListener(this);
        }
    }

    void OnDisable()
    {
        foreach (var eventResponseTuple in Events)
        {
            eventResponseTuple.Event.DeregisterListener(this);
        }
    }

    public void OnEventRaised(Event raisedEvent)
    {
        Events.Find(x => x.Event == raisedEvent)?.Response?.Invoke();
    }

    [Serializable]
    public class EventResponseTuple
    {
        public Event Event;
        public UnityEvent Response;
    }
}