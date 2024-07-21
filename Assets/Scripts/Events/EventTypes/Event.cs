using UnityEditor;
using UnityEngine;

// Event
[CreateAssetMenu(fileName = "NewEvent", menuName = "Mercadola/Events/New Event")]
public class Event : BaseEvent<Void>
{
    public void Raise() => Raise(new Void());
}

// Custom editor
[CustomEditor(typeof(Event))]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (target is not Event voidEvent) return;
        
        if (GUILayout.Button("Raise"))
        {
            voidEvent.Raise();
        }
    }
}
