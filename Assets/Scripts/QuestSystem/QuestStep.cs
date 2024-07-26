using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    public string Title;
    public QuestType Type;
    public bool QuestDidEnd = false;
    public Event OnQuestBeginEvent;
    Event OnQuestEndEvent;

    protected void StartQuestStep()
    {
        Debug.Log("You started the Quest!");
        OnQuestBeginEvent.Raise();   
    }

    protected void FinishQuestStep()
    {
        if (QuestDidEnd) { 
            // TODO: advance to next step
            OnQuestEndEvent.Raise(); 
            Debug.Log("You finished the Quest!");
        }
    }
}
