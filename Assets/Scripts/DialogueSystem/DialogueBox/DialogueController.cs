using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    DialogueManager dialogueManager;
    
    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void HandleInteraction(InputAction.CallbackContext context)
    {
        if (!dialogueManager.IsDialogueOngoing) return;
        
        if (!context.performed) return;
        
        dialogueManager.DisplayNextSentence();
    }
}