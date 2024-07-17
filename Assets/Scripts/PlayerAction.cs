using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Controller controller;
    List<GameObject> collisionObjects;

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        collisionObjects = new();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.MainAction.IsPressed()) { mainAction(); };
    }

    void mainAction() {
        var npcs = collisionObjects.Select(x => x).Where(x => x.gameObject.CompareTag("npc")).ToList();
        if(npcs.Count > 0) {
            npcs[0].GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D) {
        Debug.Log("OnTriggerEnter2D");
        if(collider2D.CompareTag("item")) {
            Destroy(collider2D.gameObject);
            // TODO: increment score
            return;
        }
        collisionObjects.Add(collider2D.gameObject);
    }
        
    void OnTriggerExit2D(Collider2D collider2D) {
        Debug.Log("OnTriggerExit2D");
        collisionObjects.Remove(collider2D.gameObject);
    }
}
