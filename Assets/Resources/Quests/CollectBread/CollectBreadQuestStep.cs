using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBreadQuestStep : QuestStep
{
    public Scorer Scorer;

    void Start()
    {
        StartQuestStep();
    }

    void Update()
    {
        if (Scorer.Score >= 500) {
            FinishQuestStep();
        }
    }
}
