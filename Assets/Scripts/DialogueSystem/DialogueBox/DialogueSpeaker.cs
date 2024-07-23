using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueSpeaker
{
    AudioSource audioSource;
    int baseHash;
    
    public DialogueSpeaker(AudioSource audioSource, int baseHash)
    {
        this.audioSource = audioSource;
        this.baseHash = baseHash;
    }
    
    public void PlayLetter(string letter, Dialogue.Emotion emotion, DialogueVoice voice) => PlayLetter(letter[0], emotion, voice);

    public void PlayLetter(char letter, Dialogue.Emotion emotion, DialogueVoice voice)
    {
        SetAudioSourceModifiers(audioSource, letter, voice, emotion);
        
        var letterSound = DetermineLetterSound(letter, voice, emotion);
        
        if (voice.truncateSoundClip)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(letterSound);
    }

    AudioClip DetermineLetterSound(char letter, DialogueVoice voice, Dialogue.Emotion emotion)
    {
        var sounds = emotion switch
        {
            Dialogue.Emotion.Normal => voice.normalSoundClips,
            Dialogue.Emotion.Hyped => voice.hypedSoundClips.Length > 0 ? voice.hypedSoundClips : voice.normalSoundClips,
            _ => voice.normalSoundClips
        };

        return sounds[HashLetter(letter) % sounds.Length];
    }
    
    void SetAudioSourceModifiers(AudioSource audioSource, char letter, DialogueVoice voice, Dialogue.Emotion emotion)
    {
        audioSource.volume = voice.volume;
        
        // Play with pitch
        var basePitch = Random.Range(voice.minPitch, voice.maxPitch);
        var hypeCharMod = char.IsUpper(letter) || letter == '!' ? 0.15f : 0f;
        var hypeEmotionMod = emotion == Dialogue.Emotion.Hyped ? 0.15f : 0f;
        var pitchMod = 1 + hypeCharMod + hypeEmotionMod;
        
        var minPitch = voice.minPitch + hypeEmotionMod + hypeCharMod;
        var maxPitch = voice.maxPitch + hypeEmotionMod + hypeCharMod;
        
        audioSource.pitch = Math.Clamp(basePitch * pitchMod, minPitch, maxPitch);
    }
    
    int HashLetter(char letter)
    {
        return (baseHash << 5) + baseHash + letter;
    }
}