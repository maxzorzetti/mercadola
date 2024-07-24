using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public int maxSentenceLength = 400;
	public int baseVoiceHash = 1000;
	public DialogueEvent OnDialogueEvent;

	// Visuals
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Animator dialogueBoxAnimator;
	public Animator leftPortraitAnimator;
	public Animator CameraAnimator;
	
	// Sound
	DialogueSpeaker speaker;

	// Dialogue logic
	[HideInInspector] public Dialogue CurrentDialogue;
	public bool IsDialogueOngoing => CurrentDialogue != null;
	Queue<(string sentence, Dialogue.Speech speech)> sentences;
	bool isTyping => typingTask != null && !typingTask.IsCompleted; 
	string currentSentence;
	Task typingTask;
	CancellationTokenSource typingCTS;
	
	// Use this for initialization
	void Start () {
		sentences = new Queue<(string sentence, Dialogue.Speech speech)>();
		CurrentDialogue = null;
		
		speaker = TryGetComponent<AudioSource>(out var component) 
			? new DialogueSpeaker(component, baseVoiceHash) 
			: new DialogueSpeaker(gameObject.AddComponent<AudioSource>(), baseVoiceHash);

		if (OnDialogueEvent == null)
		{
			Debug.LogWarning($"DialogueManager '{name}' is missing an OnDialogueEvent event");
		}
	}

	public void StartDialogue(Dialogue dialogue)
	{
		if (IsDialogueOngoing) return;
		CurrentDialogue = dialogue;
		
		leftPortraitAnimator.runtimeAnimatorController = dialogue.portrait;
		dialogueBoxAnimator.SetBool("IsOpen", true);
		CameraAnimator.SetBool("Dialog", true);

		nameText.text = dialogue.speaker;

		sentences.Clear();

		EnqueueDialogue(dialogue);

		DisplayNextSentence();

		OnDialogueEvent.Raise(new DialogueEventData(dialogue, isDialogueEnd: false));
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
		
		typingTask = TypeSpeech(sentence, speech, CurrentDialogue.voice, typingCTS);
	}

	void EnqueueDialogue(Dialogue dialogue)
	{
		CurrentDialogue = dialogue;
		
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

	async Task TypeSpeech(string sentence, Dialogue.Speech speech, DialogueVoice voice, CancellationTokenSource cts)
	{
		currentSentence = sentence;
		dialogueText.text = "";
		leftPortraitAnimator.SetBool("IsTalking", true);

		if (speech.emotion == Dialogue.Emotion.Hyped)
			GetComponent<Spinner>().Spin();
		else 
			GetComponent<Spinner>().StopSpin();
		
		// Loop through sentence chars
		foreach (var (letter, i) in sentence.Select((value, i) => (value, i)))
		{
			if (voice != null && !char.IsWhiteSpace(letter) && i % voice.frequency == 0)
			{
				speaker.PlayLetter(letter, speech.emotion, voice);
			}
			
			var waitTime = 1000 / speech.speed;
			await Task.Delay((int)waitTime);
			if (cts.Token.IsCancellationRequested) break;

			dialogueText.text += letter;		
		}

		leftPortraitAnimator.SetBool("IsTalking", false);
		cts.Token.ThrowIfCancellationRequested();
		
		if (speech.autoSkip)
		{
			typingTask = null;
			DisplayNextSentence();
		}
	}

	void EndDialogue()
	{
		dialogueBoxAnimator.SetBool("IsOpen", false);
		CameraAnimator.SetBool("Dialog", false);
		// When the animation finishes, it triggers the OnDialogueBoxClosed method below
	}
	
	void OnDialogueBoxClosed(int i)
	{
		FinishDialogue();
	}

	public void FinishDialogue()
	{
		// Adding this gambiarra check because this animation is triggered as soon as the scene loads 
		if (!IsDialogueOngoing) return;
		
		var dialogue = CurrentDialogue;
		CurrentDialogue = null;
		OnDialogueEvent.Raise(new DialogueEventData(dialogue, isDialogueEnd: true));
	}
}
