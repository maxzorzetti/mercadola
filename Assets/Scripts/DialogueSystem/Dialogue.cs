using System;
using UnityEditor.Animations;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Mercadola/Dialogue/New Dialogue")]
public class Dialogue : ScriptableObject 
{
	public string speaker;
	
	public AnimatorController portrait;
	
	public DialogueVoice voice;

	public Speech[] speeches;

	[Serializable]
	public class Speech
	{
		[Range(1, 300)]
		public float speed = 30f;
		public Emotion emotion = Emotion.Normal;
		public bool autoSkip; 
			
		[TextArea(5, 10)]
		public string sentence;
	}
	
	[Serializable]
	public enum Emotion
	{
		Normal, Hyped  
	}
}
