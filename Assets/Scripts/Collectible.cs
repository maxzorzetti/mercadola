using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Event CollectEvent;

    void Start()
    {
        if (CollectEvent == null)
        {
            Debug.LogWarning($"Object {name} is missing an OnCollectEvent!");
        }
    }
    
    public void Collect()
    {
        CollectEvent.Raise();
        Destroy(gameObject);
    }
}
