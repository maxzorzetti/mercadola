using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectEvent CollectEvent;

    [Range(0f, 2f)]
    public float CollectionTime = 0.25f;

    public Transform ShrinkTarget;
    
    float collectionSpeed => 1 / CollectionTime;
    
    
    public AnimationCurve CollectionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    bool isCollected;
    Progression collectProgression;
    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // Get a sprite to shrink if none was set
        ShrinkTarget ??= GetComponentInChildren<SpriteRenderer>().transform; 
        collectProgression = new Progression();
        
        if (CollectEvent == null)
        {
            Debug.LogWarning($"Object {name} is missing an OnCollectEvent!");
        }
    }
    
    public void Collect()
    {
        if (isCollected) return;
        isCollected = true;
        CollectEvent.Raise(new CollectEventData(this));
    }

    void Update()
    {
        if (isCollected) DoCollection();
    }

    void DoCollection()
    {
        CollectionCurve.Evaluate(collectProgression.ProgressRate);
        ShrinkTarget.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, collectProgression.ProgressRate);
        
        collectProgression.Advance(Time.deltaTime * collectionSpeed);
        if (collectProgression.Consume())
        {
            Destroy(gameObject);
        }
    }
}
