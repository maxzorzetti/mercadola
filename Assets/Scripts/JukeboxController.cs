using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class JukeboxController : MonoBehaviour
{
    ParticleSystem teteuzinho;
    Animator radioAnimation;
    AudioSource musicPlayer;
    public AudioClip[] music;
    int selectedMusic = 0;
    static readonly int Radio = Animator.StringToHash("Radio");

    void Awake()
    {
         teteuzinho = GetComponentInChildren<ParticleSystem>();
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        teteuzinho.Play();
        musicPlayer.Play();
        radioAnimation.SetBool(Radio, true);
    }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            teteuzinho.Stop();
            musicPlayer.Pause();
            radioAnimation.SetBool(Radio, false);
        }
}
