using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class JukeboxController : MonoBehaviour
{
    ParticleSystem musicNotes;
    Animator radioAnimation;
    AudioSource musicPlayer;
    public AudioClip[] music;
    public BoolEvent OnPlayerIsNear;
    int selectedMusic = 0;

    static readonly int Radio = Animator.StringToHash("Radio");

    bool isPlaying = false;

    void Awake()
    {
         musicNotes = GetComponentInChildren<ParticleSystem>();
         radioAnimation = GetComponent<Animator>();
         musicPlayer = GetComponentInChildren<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer.clip = music[selectedMusic];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ToggleRadio()
    {
        if(!isPlaying)
        {
            musicNotes.Play();
            musicPlayer.Play();
            radioAnimation.SetBool(Radio, true);
        }
        else
        {
            musicNotes.Stop();
            musicPlayer.Pause();
            radioAnimation.SetBool(Radio, false);
        }
        isPlaying = !isPlaying;
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        {
            OnPlayerIsNear.Raise(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        {
            OnPlayerIsNear.Raise(false);
        }
    }
}


