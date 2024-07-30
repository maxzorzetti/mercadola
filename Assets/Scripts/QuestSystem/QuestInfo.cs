using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "ScriptableObjects/QuestInfo", order = 1)]
public class QuestInfo: ScriptableObject 
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public QuestInfo prerequisites;

    [Header("Steps")]
    public GameObject[] steps;

    [Header("Rewards")]
    public int moneyReward;
    public int experienceReward;

    private void OnValidate() 
    {
        // id is the name of the SO asset
        #if UNITY_EDITOR
        id = name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}

public enum QuestType 
{
    Gather,
    Investigation,
    Quiz,
    Battle
}