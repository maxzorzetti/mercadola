using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public int maxSentenceLength = 400;

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Animator dialogueBoxAnimator;
	public Animator leftPortraitAnimator;

	Queue<(string sentence, Dialogue.Speech speech)> sentences;

	public bool IsDialogueOngoing;
	bool isTyping => typingTask != null && !typingTask.IsCompleted; 
	string currentSentence;
	
	Task typingTask;
	CancellationTokenSource typingCTS;
	
	// Use this for initialization
	void Start () {
		sentences = new Queue<(string sentence, Dialogue.Speech speech)>();
		IsDialogueOngoing = false;
	}

	public void StartDialogue(Dialogue dialogue)
	{
		if (IsDialogueOngoing) return;
		IsDialogueOngoing = true;
		
		dialogueBoxAnimator.SetBool("IsOpen", true);

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
			typingCTS.Cancel();
			dialogueText.text = currentSentence;
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
		leftPortraitAnimator.SetBool("IsTalking", true);
		foreach (var letter in sentence.ToCharArray())
		{
			var waitTime = 1000 / speed;
			await Task.Delay((int)waitTime);
			if (cts.Token.IsCancellationRequested) break;

			dialogueText.text += letter;
		}
		leftPortraitAnimator.SetBool("IsTalking", false);
		cts.Token.ThrowIfCancellationRequested();
		
		if (shouldAutoSkip)
		{
			typingTask = null;
			DisplayNextSentence();
		}
	}

	void EndDialogue()
	{
		dialogueBoxAnimator.SetBool("IsOpen", false);
		Task.Delay(300).ContinueWith(_ => IsDialogueOngoing = false);
	}
}
