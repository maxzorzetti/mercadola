using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfo questInfo;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private bool playerIsNear = false;
    private QuestManager questManager;
    private QuestState currentQuestState;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void OnInvestigate()
    {
        if(!playerIsNear) 
        {
            return;
        }
        else
        {
            if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
            {
                questManager.StartQuest(questInfo.id);
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            {
                questManager.FinishQuest(questInfo.id);
                Destroy(this);
            }
        }
    }

    public void QuestStateChange(Quest quest)
    {
        if(quest.Info.id.Equals(questInfo.id))
        {
            currentQuestState = quest.State;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
