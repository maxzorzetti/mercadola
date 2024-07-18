using UnityEngine;
using UnityEngine.Events;

public class Listener : MonoBehaviour
{
    public Event Event;
    public UnityEvent Response;

    void OnEnable()
    {
        Event.RegisterListener(this);
    }

    void OnDisable()
    {
        Event.DeregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response?.Invoke();
    }
}