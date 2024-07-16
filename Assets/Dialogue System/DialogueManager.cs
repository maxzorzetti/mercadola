using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public int maxSentenceLength = 400;
	public Text nameText;
	public Text dialogueText;
	public Animator animator;

	Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		EnqueueDialogue(dialogue);

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	void EnqueueDialogue(Dialogue dialogue) 
	{
		foreach (var sentence in dialogue.sentences)
		{
			// split it into multiple sentences if it's too long
			if (sentence.Length < maxSentenceLength) 
			{
				sentences.Enqueue(sentence);
			} 
			else 
			{
				var charactersLeft = sentence.Length;
				var characterAnchorPosition = 0;
				while (charactersLeft > 0) 
				{
					var sentenceToAdd = sentence.Substring(characterAnchorPosition, Mathf.Min(maxSentenceLength, charactersLeft));
					sentences.Enqueue(sentenceToAdd);
					charactersLeft -= maxSentenceLength;
					characterAnchorPosition += maxSentenceLength;
				}
			}
		}
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (var letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

}
