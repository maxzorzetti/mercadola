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
        if (controller.MainAction.IsPressed()) { MainAction(); }
    }

    void MainAction() {
        if (triggers["npc"].Count == 0) return;
        
        triggers["npc"][0].GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    void OnTriggerEnter2D(Collider2D collider2D) {
        switch (collider2D.tag)
        {
            case "npc":
                triggers["npc"].Add(collider2D);
                break;    
            case "item":
                Destroy(collider2D.gameObject);
                // TODO: increment score
                break;
        }
    }
        
    void OnTriggerExit2D(Collider2D collider2D) {
        triggers.TryGetValue(collider2D.tag, out var triggerList);
        triggerList?.Remove(collider2D);
    }
}
