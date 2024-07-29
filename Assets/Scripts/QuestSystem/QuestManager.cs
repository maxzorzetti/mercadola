using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    public QuestEvents QuestEvents;
    public Scorer Scorer;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }
    
    private void Start()
    {
        // Broadcasts initial state of every quest on startup
        foreach (Quest quest in questMap.Values)
        {
            QuestEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if(quest.State == QuestState.REQUIEREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest, QuestState.CAN_START);
            }
        }
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        // TODO: create logic for requirements
        return true;
    }

      public void ChangeQuestState(Quest quest, QuestState newState)
    {
        quest.State = newState;
        QuestEvents.QuestStateChange(quest);
    }

    public void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.StartQuest(transform);
        ChangeQuestState(quest, QuestState.IN_PROGRESS);
    }

    public void AdvanceQuest(string id)
    {
        Debug.Log("AdvanceQuest: " + id);
    }

    public void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.FinishQuest();
        ChangeQuestState(quest, QuestState.FINISHED);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests/CollectBread");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfo info in allQuests)
        {
            if(idToQuestMap.ContainsKey(info.id))
            {
                Debug.Log("Duplicate ID found when creating quest map: " + info.id);
            }
            idToQuestMap.Add(info.id, new Quest(info));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id) 
    {   
        Quest quest = questMap[id];
        if(quest == null)
        {
            Debug.Log("ID not found in the Quest map: " + id);
        }
        return quest;
    }
}
