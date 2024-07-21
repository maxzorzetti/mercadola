using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Event OnInteractEvent;
    public Event OnInvestigateEvent;
    public Event OnAmazeEvent;
    
    Dictionary<string, List<Collider2D>> triggers;
    List<Interactable> interactablesInRange;

    // Start is called before the first frame update
    void Start()
    {
        interactablesInRange = new List<Interactable>();
        triggers = new Dictionary<string, List<Collider2D>>
        {
            ["npc"] = new(),
            ["item"] = new(),
            ["wall"] = new()
        };
    }

    // Action Methods
    public void Interact() {
        if (interactablesInRange.Count == 0) return;

        var closestInteractable = interactablesInRange
            .OrderBy(interactable => Vector2.Distance(transform.position, interactable.transform.position))
            .First();
        
        closestInteractable.Interact();
        
        OnInteractEvent.Raise();
    }
    
    public void Investigate()
    {
        OnInvestigateEvent.Raise();
    }
    
    public void Amaze()
    {
        OnAmazeEvent.Raise();
    }

    // Collider Overrides

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Interactable interactable)) {
            interactablesInRange.Add(interactable);
        }
        
        switch (collider2D.tag)
        {
            case "npc":
                triggers["npc"].Add(collider2D);
                break;    
            case "item":
                triggers["item"].Add(collider2D);
                break;
        }
    }
        
    void OnTriggerExit2D(Collider2D collider2D) {
        triggers.TryGetValue(collider2D.tag, out var triggerList);
        triggerList?.Remove(collider2D);
    }
}
