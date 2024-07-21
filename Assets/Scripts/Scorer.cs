using UnityEngine;

public class Scorer : MonoBehaviour
{
    public IntegerEvent OnScoreIncreaseEvent;
    
    [Range(1, 1000)]
    public int ScorePerBread = 100;

    int score;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        if (OnScoreIncreaseEvent == null)
        {
            Debug.LogWarning($"Scorer '{name}' is missing an event");
        }
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log($"Score is now {score}!");
        OnScoreIncreaseEvent.Raise(amount);
    }
    
    public void HandleOnCollectEvent(CollectEventData data)
    {
        if (data.Collectible.name.Contains("Bread"))
        {
            IncreaseScore(ScorePerBread);    
        }
    }
}
