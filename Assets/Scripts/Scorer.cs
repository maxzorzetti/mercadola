using UnityEngine;

public class Scorer : MonoBehaviour
{
    public IntegerEvent OnScoreIncreaseEvent;
    
    [Range(1, 1000)]
    public int ScorePerBread = 100;

    public int Score;
    
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        if (OnScoreIncreaseEvent == null)
        {
            Debug.LogWarning($"Scorer '{name}' is missing an OnScoreIncreaseEvent event");
        }
    }
    
    public void IncreaseScore(int amount)
    {
        Score += amount;
        Debug.Log($"Score is now {Score}!");
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
