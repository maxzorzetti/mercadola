using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfo Info;
    public QuestState State;
    private int currentQuestStepIndex;

    public Quest(QuestInfo info) 
    {
        Info = info;
        State = QuestState.REQUIEREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
    }

    public void MoveToNextStep() 
    {
        currentQuestStepIndex++;
    }

    public bool CurrentQuestStepExists() 
    {
        return currentQuestStepIndex < Info.steps.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStep = GetCurrentQuestStep();
        
        if (questStep != null)
        {
            Object.Instantiate<GameObject>(questStep, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStep()
    {
        GameObject stepPrefab = null;

        if(CurrentQuestStepExists()) 
        {
            stepPrefab =  Info.steps[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Error: Quest (id = " + Info.id + ") step " + currentQuestStepIndex + 
            " out of range [last step is " + Info.steps.Length + "]!");
        }

        return stepPrefab;
    }
}
