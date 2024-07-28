using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfo infoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;

    public QuestEvents QuestEvents;

    private void Awake()
    {
        questId = infoForPoint.id;
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
                QuestEvents.StartQuest(questId);
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            {
                QuestEvents.FinishQuest(questId);
            }
        }
    }

    public void QuestStateChange(Quest quest)
    {
        if(quest.Info.id.Equals(questId))
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
