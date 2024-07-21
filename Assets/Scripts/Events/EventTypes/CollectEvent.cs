using System;
using UnityEngine;
using UnityEngine.Events;

// Event
[CreateAssetMenu(fileName = "NewCollectibleEvent", menuName = "Mercadola/Events/New Collectible Event")]
public class CollectEvent : BaseEvent<CollectEventData> { }

// Custom data
[Serializable]
public class CollectEventData
{
    public Collectible Collectible;
    
    public CollectEventData(Collectible collectible)
    {
        Collectible = collectible;
    }
}