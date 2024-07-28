using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents: MonoBehaviour
{
    public StringEvent OnQuestStart;
    public StringEvent OnQuestAdvance;
    public StringEvent OnQuestFinish;
    public QuestEvent OnQuestStateChange;

    public void StartQuest(string id) 
    {
        Debug.Log("StartQuest Raise with id: " + id);
        OnQuestStart.Raise(id);
    }
    
    public void AdvanceQuest(string id) 
    {
        OnQuestAdvance.Raise(id);
    }
    
    public void FinishQuest(string id) 
    {
        OnQuestFinish.Raise(id);
    }
    
    public void QuestStateChange(Quest quest) 
    {
        OnQuestStateChange.Raise(quest);
    }
}
