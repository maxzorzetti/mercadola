using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBreadQuestStep : QuestStep
{
    public Scorer Scorer;

    void Start()
    {
        StartQuestStep();
        Scorer = FindObjectOfType<Scorer>();
    }

    void Update()
    {
        if (Scorer.Score >= 500) {
            FinishQuestStep();
        }
    }
}
