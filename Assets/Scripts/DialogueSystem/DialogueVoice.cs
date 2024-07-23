using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewVoice", menuName = "Mercadola/Dialogue/New Voice")]
public class DialogueVoice : ScriptableObject
{
    [SerializeField] public AudioClip[] normalSoundClips;
    [SerializeField] public AudioClip[] hypedSoundClips;
    [Range(0, 1)]
    [SerializeField]
    public float volume = 1.0f;
    [SerializeField] public bool truncateSoundClip;
    
    [Header("Voice Modifiers")]
    
    [Range(1, 7)]
    [SerializeField]
    public int frequency = 3;
    [Range(-3, 3)]
    [SerializeField]
    public float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField]
    public float maxPitch = 3f;
}