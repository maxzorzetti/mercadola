using System;
using UnityEngine;

// Event
[CreateAssetMenu(fileName = "NewDialogueEvent", menuName = "Mercadola/Events/New Dialogue Event")]
public class DialogueEvent : BaseEvent<DialogueEventData> { }

// Custom data 
[Serializable]
public class DialogueEventData
{
    public Dialogue Dialogue;
    public bool IsDialogueEnd;

    public DialogueEventData(Dialogue dialogue, bool isDialogueEnd)
    {
        Dialogue = dialogue;
        IsDialogueEnd = isDialogueEnd;
    }
}

