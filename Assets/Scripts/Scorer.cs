using UnityEngine;

public class Scorer : MonoBehaviour
{
    public Event ScoreIncreaseEvent;
    
    [Range(1, 1000)]
    public int ScorePerBread = 100;

    int score;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        if (ScoreIncreaseEvent == null)
        {
            Debug.LogWarning($"Object {name} is missing an OnCollectEvent!");
        }
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        ScoreIncreaseEvent.Raise();
        Debug.Log($"Score is now {score}!");
    }
    
    public void HandleOnCollectedEvent() => IncreaseScore(ScorePerBread);
}
