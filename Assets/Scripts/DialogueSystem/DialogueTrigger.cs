using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	
	public Event DialogueEvent;
	
	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		var hasDialogueStarted = FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		if (hasDialogueStarted && DialogueEvent != null) DialogueEvent.Raise();
	}

}
