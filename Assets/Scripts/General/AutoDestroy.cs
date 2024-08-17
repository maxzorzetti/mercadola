using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 1f;
    public bool beginOnStart = true;

    Progression lifetimeTimer;
    bool destroy;

    void Start()
    {
        lifetimeTimer = new Progression(lifetime);

        destroy = beginOnStart;
    }

    public void Destroy()
    {
        destroy = true;
    }
    
    void Update()
    {
        if (!destroy) return;
        
        if (lifetimeTimer.AdvanceAndConsume())
        {
            Destroy(gameObject);
        }
    }
}