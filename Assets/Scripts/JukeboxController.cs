using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class JukeboxController : MonoBehaviour
{
    ParticleSystem musicNotes;
    Animator radioAnimation;
    AudioSource musicPlayer;

    public AudioClip staticAudio;
    public AudioClip[] music;
    public BoolEvent OnPlayerIsNear;
    int selectedMusic = 0;

    float musicTimeTemp;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ToggleRadio()
    {
        if(!isPlaying)
        {
            StartCoroutine(PlayMusic(true));
        }
        else
        {
            StartCoroutine(PlayMusic(false));
        }
        isPlaying = !isPlaying;
    }

    
    IEnumerator PlayMusic(bool isOn)
    {
        if (isOn)
        {
            musicPlayer.clip = staticAudio;
            //wait0.5s,then play static
            radioAnimation.SetBool(Radio, true);
            musicPlayer.Play();
            yield return new WaitForSeconds(1f);
            //wait 1s, then play music

            // Play Particles
            musicNotes.Play();

            // Play Music
            musicPlayer.Stop();
            musicPlayer.clip = music[selectedMusic];
            musicPlayer.time = musicTimeTemp;
            musicPlayer.Play();
        } else
        {
            musicTimeTemp = musicPlayer.time;
            musicPlayer.Stop();
            musicNotes.Stop();
            radioAnimation.SetBool(Radio, false);
        }
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


