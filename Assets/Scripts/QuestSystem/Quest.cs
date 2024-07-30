using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfo Info;
    public QuestState State;
    private int currentQuestStepIndex;
    private Transform parentTransform;

    public Quest(QuestInfo info) 
    {
        Info = info;
        State = QuestState.REQUIEREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
    }

    public void StartQuest(Transform parentTransform)
    {
        Debug.Log("StartQuest: " + Info.id);
        this.parentTransform = parentTransform;
        InstantiateCurrentQuestStep();
    }

    public void FinishQuest()
    {
        Debug.Log("FinishQuest: " + Info.id);
    }

    public void UpdateProgression() 
    {
        currentQuestStepIndex++;

        // Either move to next step or change state
        if (GetCurrentQuestStep() != null) 
        {
            InstantiateCurrentQuestStep();
        } 
        else
        {
            GameObject.FindObjectOfType<QuestManager>().ChangeQuestState(this, QuestState.CAN_FINISH);
        }
    }

    public bool CurrentQuestStepExists() 
    {
        return currentQuestStepIndex < Info.steps.Length;
    }

    public void InstantiateCurrentQuestStep()
    {
        GameObject questStep = GetCurrentQuestStep();
        
        if (questStep != null)
        {
            var questStepObject = Object.Instantiate(questStep, parentTransform);
            questStepObject.GetComponent<QuestStep>().OnStepFinish += UpdateProgression;
        }
    }

    private GameObject GetCurrentQuestStep()
    {
        GameObject stepPrefab = null;

        if(CurrentQuestStepExists()) 
        {
            stepPrefab =  Info.steps[currentQuestStepIndex];
        }

        return stepPrefab;
    }
}
