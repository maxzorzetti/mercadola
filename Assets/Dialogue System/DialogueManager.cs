using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public int maxSentenceLength = 400;

	public Text nameText;
	public Text dialogueText;
	public Animator animator;

	Queue<(string sentence, Dialogue.Speech speech)> sentences;
	
	Task typingTask;
	CancellationTokenSource typingCTS;

	bool isTyping => typingTask != null && !typingTask.IsCompleted; 
	string currentSentence;
	
	// Use this for initialization
	void Start () {
		sentences = new Queue<(string sentence, Dialogue.Speech speech)>();
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
		if (sentences.Count == 0 && !isTyping)
		{
			EndDialogue();
			return;
		}
		
		if (isTyping) 
		{
			dialogueText.text = currentSentence;
			typingCTS.Cancel();
			return;
		}

		var (sentence, speech) = sentences.Dequeue();
		typingCTS = new CancellationTokenSource();
		typingTask = TypeSentence(sentence, speech.speed, speech.emotion, speech.autoSkip, typingCTS);
	}

	void EnqueueDialogue(Dialogue dialogue) 
	{
		foreach (var speech in dialogue.speeches)
		{
			// split it into multiple sentences if it's too long
			if (speech.sentence.Length < maxSentenceLength) 
			{
				sentences.Enqueue((speech.sentence, speech));
			} 
			else 
			{
				var charactersLeft = speech.sentence.Length;
				var characterAnchorPosition = 0;
				while (charactersLeft > 0) 
				{
					var sentence = speech.sentence.Substring(characterAnchorPosition, Mathf.Min(maxSentenceLength, charactersLeft));
					sentences.Enqueue((sentence, speech));
					charactersLeft -= maxSentenceLength;
					characterAnchorPosition += maxSentenceLength;
				}
			}
		}
	}

	async Task TypeSentence(string sentence, float speed, Dialogue.Emotion emotion, bool shouldAutoSkip, CancellationTokenSource cts)
	{
		currentSentence = sentence;
		dialogueText.text = "";
		
		foreach (var letter in sentence.ToCharArray())
		{
			var waitTime = 1000 / speed;
			await Task.Delay((int)waitTime);
			cts.Token.ThrowIfCancellationRequested();
			dialogueText.text += letter;
		}

		if (shouldAutoSkip)
		{
			typingTask = null;
			DisplayNextSentence();
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}
}
