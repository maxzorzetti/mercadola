using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    public string Title;
    public QuestType Type;
    public event Action OnStepStart;
    public event Action OnStepFinish;

    protected void StartQuestStep()
    {
        OnStepStart?.Invoke();
    }

    protected void FinishQuestStep()
    {
        OnStepFinish?.Invoke();
        Destroy(this);
    }
}
