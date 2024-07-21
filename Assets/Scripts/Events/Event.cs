using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Mercadola/New Event")]
public class Event : ScriptableObject
{
    List<Listener> listeners = new List<Listener>();

    public void Raise()
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(this);
        }
    }

    public void RegisterListener(Listener listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListener(Listener listener)
    {
        listeners.Remove(listener);
    }
}

[CustomEditor(typeof(Event))]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Event)target;

        if (GUILayout.Button("Raise"))
        {
            script.Raise();
        }
    }
}

