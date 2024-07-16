using System;
using UnityEngine;

[Serializable]
public class Dialogue {

	public string name;

	public Speech[] speeches;

	[Serializable]
	public class Speech
	{
		[Range(1f, 200f)]
		public float speed = 50;
		public Emotion emotion = Emotion.Normal;
		public bool autoSkip; 
			
		[TextArea(3, 10)]
		public string sentence;
	}
	
	[Serializable]
	public enum Emotion
	{
		Normal, Hyped  
	}
}
