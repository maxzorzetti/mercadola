using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Dictionary<string, List<Collider2D>> triggers;
    
    public Event OnInteractEvent;
    public Event OnInvestigateEvent;
    public Event OnAmazeEvent;

    // Start is called before the first frame update
    void Start()
    {
        triggers = new Dictionary<string, List<Collider2D>>
        {
            ["npc"] = new(),
            ["item"] = new(),
            ["wall"] = new()
        };
    }

    // Action Methods
    public void Interact() {
        if (triggers["npc"].Count == 0) return;
        
        triggers["npc"][0].GetComponent<DialogueTrigger>().TriggerDialogue();
        
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

    void OnTriggerEnter2D(Collider2D collider2D) {
        switch (collider2D.tag)
        {
            case "npc":
                triggers["npc"].Add(collider2D);
                break;    
            case "item":
                triggers["item"].Add(collider2D);
                collider2D.GetComponent<Collectible>()?.Collect();
                break;
        }
    }
        
    void OnTriggerExit2D(Collider2D collider2D) {
        triggers.TryGetValue(collider2D.tag, out var triggerList);
        triggerList?.Remove(collider2D);
    }
}
