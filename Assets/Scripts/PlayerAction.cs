using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Controller controller;
    Dictionary<string, List<Collider2D>> triggers;

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        triggers = new Dictionary<string, List<Collider2D>>
        {
            ["npc"] = new()
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.MainAction.DidPress()) { MainAction(); }
        if (!controller.IsHoldingMovementKeys()) { InvestigateAction(); }
    }

    // Action Methods
    void MainAction() {
        if (triggers["npc"].Count == 0) return;
        
        triggers["npc"][0].GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    void InvestigateAction() {
        if(controller.Investigate.DidPress()) {
            GetComponentInChildren<Animator>().ResetTrigger("Exit");
            GetComponentInChildren<Animator>().SetTrigger("Investigate");
        } else if (!controller.Investigate.IsHold()) {
            GetComponentInChildren<Animator>().ResetTrigger("Investigate");
            GetComponentInChildren<Animator>().SetTrigger("Exit");
        }
    }

    // Collider Overrides

    void OnTriggerEnter2D(Collider2D collider2D) {
        switch (collider2D.tag)
        {
            case "npc":
                triggers["npc"].Add(collider2D);
                break;    
            case "item":
                collider2D.GetComponent<Collectible>().Collect();
                break;
        }
    }
        
    void OnTriggerExit2D(Collider2D collider2D) {
        triggers.TryGetValue(collider2D.tag, out var triggerList);
        triggerList?.Remove(collider2D);
    }
}
