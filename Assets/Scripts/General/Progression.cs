using System;

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
    
    public void Reset() => CurrentProgress = 0;
    
    bool CheckCompletion() => CurrentProgress >= MaxProgress;
}