using System;
using UnityEngine;

public class Progression
{
    public float MaxProgress { get; private set; }
    public float CurrentProgress { get; private set; }
    public float ProgressRate => CurrentProgress / MaxProgress;
    public bool IsComplete => CurrentProgress >= MaxProgress;
    
    public event Action OnProgressComplete;
    
    public Progression(float maxProgress = 1)
    {
        MaxProgress = maxProgress;
    }
    
    public void Advance()
    {
        Advance(Time.deltaTime);
    }

    public void Advance(float progress)
    {
        CurrentProgress += progress;

        if (CheckCompletion())
        {
            OnProgressComplete?.Invoke();
        }
    }

    public bool Consume()
    {
        if (CheckCompletion())
        {
            CurrentProgress -= MaxProgress;
            return true;
        }
        return false;
    }
    
    public bool AdvanceAndConsume()
    {
        return AdvanceAndConsume(Time.deltaTime);
    }

    public bool AdvanceAndConsume(float progress)
    {
        Advance(progress);
        return Consume();
    }
    
    public bool AdvanceAndConsume(out bool didConsume)
    {
        Advance();
        didConsume = Consume();
        
        return didConsume;
    }

    public bool AdvanceAndConsume(float progress, out bool didConsume)
    {
        Advance(progress);
        didConsume = Consume();
        
        return didConsume;
    }

    public void Retract(float hindrance)
    {
        CurrentProgress = Math.Max(0, CurrentProgress - hindrance);
    }
    
    public void Reset() => CurrentProgress = 0;
    
    bool CheckCompletion() => CurrentProgress >= MaxProgress;
}