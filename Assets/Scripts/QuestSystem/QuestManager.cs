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

    private void Awake()
    {
        questMap = CreateQuestMap();
        QuestEvents = new();
    }
    
    private void Start()
    {
        // Broadcasts initial state of every quest on startup
        foreach (Quest quest in questMap.Values)
        {
            QuestEvents.QuestStateChange(quest);
        }
    }

    private void StartQuest(string id)
    {
        Debug.Log("StartQuest: " + id);
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log("AdvanceQuest: " + id);
    }

    private void FinishQuest(string id)
    {
        Debug.Log("FinishQuest: " + id);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");

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
