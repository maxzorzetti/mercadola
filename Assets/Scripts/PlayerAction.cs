using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Dictionary<string, List<Collider2D>> triggers;
    
    public event System.Action OnInteract;
    public event System.Action OnInvestigate;
    public event System.Action OnAmaze;

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
        OnInteract?.Invoke();

        if (triggers["npc"].Count == 0) return;
        
        triggers["npc"][0].GetComponent<DialogueTrigger>().TriggerDialogue();
    }
    
    public void Investigate()
    {
        OnInvestigate?.Invoke();
    }
    
    public void Amaze()
    {
        OnAmaze?.Invoke();
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
