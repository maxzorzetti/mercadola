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
    void Awake()
    {
         teteuzinho = GetComponentInChildren<ParticleSystem>();
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Radio Music On");
        if ( other.tag == "Player" )
        {
            teteuzinho.Play();
            musicPlayer.clip = music[selectedMusic];
            musicPlayer.Play();
            radioAnimation.SetBool("Radio", true);
        }
    }

        void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Radio Music Off");
        if ( other.tag == "Player" )
        {
            teteuzinho.Stop();
            musicPlayer.Pause();
            radioAnimation.SetBool("Radio", false);
        }
    }
}
