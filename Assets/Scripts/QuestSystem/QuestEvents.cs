using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents
{
    public StringEvent OnStartQuest;
    public void StartQuest(string id) 
    {
        if(OnStartQuest != null)
        {
            OnStartQuest.Raise(id);
        }
    }

    public StringEvent OnAdvanceQuest;
    public void AdvanceQuest(string id) 
    {
        if(OnAdvanceQuest != null)
        {
            OnAdvanceQuest.Raise(id);
        }
    }

    public StringEvent OnFinishQuest;
    public void FinishQuest(string id) 
    {
        if(OnFinishQuest != null)
        {
            OnFinishQuest.Raise(id);
        }
    }

    public QuestEvent OnQuestStateChange;
    public void QuestStateChange(Quest quest) 
    {
        if(OnQuestStateChange != null)
        {
            OnQuestStateChange.Raise(quest);
        }
    }
}
