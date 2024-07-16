using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public List<AudioClip> tracks;
    
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(string bgmTrack) => PlayBGM(bgmTrack, true);

    public void PlayBGM(string bgmTrack, bool shouldStopIfPlaying)
    {
        if (shouldStopIfPlaying && audioSource.isPlaying && audioSource.clip.name.Contains(bgmTrack))
        {
            audioSource.Stop();
            return;
        }
        
        var track = tracks.Find(clip => clip.name.Contains(bgmTrack));
        audioSource.clip = track;
        audioSource.Play();
    }
}
