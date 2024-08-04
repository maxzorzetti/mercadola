using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class ToggleButtonController : MonoBehaviour
{
    Animator ToggleAnimator;
    bool ToggleAnimation = false;

    static readonly int isOn = Animator.StringToHash("isOn");

    void Awake()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
        ToggleAnimator = GetComponent<Animator>();
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
        Debug.Log(playerIsNear);
        var tempColor = GetComponent<SpriteRenderer>().color;
        tempColor.a = playerIsNear ? 1f : 0f;
        GetComponent<SpriteRenderer>().color = tempColor;   
    }

    public void ToggleAnnimation()
    {  
        ToggleAnimator.SetBool(isOn, !ToggleAnimation);
        ToggleAnimation = !ToggleAnimation;
    }
}
