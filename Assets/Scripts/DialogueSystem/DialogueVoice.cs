using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewVoice", menuName = "Mercadola/Dialogue/New Voice")]
public class DialogueVoice : ScriptableObject
{
    [SerializeField] AudioClip[] typingSoundClips;
    [Range(0, 1)]
    [SerializeField] float volume = 1.0f;
    [SerializeField] bool truncateSoundClip;
    
    [Header("Voice Modifiers")]
    
    [Range(1, 7)]
    [SerializeField] int frequency = 3;
    [Range(-3, 3)]
    [SerializeField] float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField] float maxPitch = 3f;
}