using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class ToggleButtonController : MonoBehaviour
{
    Animator ToggleAnimator;
    bool ToggleAnimation = false;
    AudioSource SFXPlayer;
    public AudioClip[] SFXs;

    static readonly int isOn = Animator.StringToHash("isOn");

    void Awake()
    {
        SFXPlayer  = GetComponent<AudioSource>();
        ToggleAnimator = GetComponent<Animator>();
        var tempstartColor = GetComponent<SpriteRenderer>().color;
        tempstartColor.a = 0f;
        GetComponent<SpriteRenderer>().color = tempstartColor;   
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DidTriggerParrentCollider(bool playerIsNear)
    {
        var tempColor = GetComponent<SpriteRenderer>().color;
        tempColor.a = playerIsNear ? 1f : 0f;
        GetComponent<SpriteRenderer>().color = tempColor;
    }

    public void ToggleAnnimation()
    {  
        ToggleAnimator.SetBool(isOn, !ToggleAnimation);
        SFXPlayer.clip = ToggleAnimation ? SFXs[0] : SFXs[1];
        ToggleAnimation = !ToggleAnimation;
    }

        void PlayButtonSound()
    {
        SFXPlayer.Play();
    }
}
