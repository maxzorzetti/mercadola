using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DogAnimator : MonoBehaviour
{
    static readonly int IsBarking = Animator.StringToHash("isBarking");
    static readonly int IsWalking = Animator.StringToHash("isWalking");
    
    Animator animator;
    // Animation animation;
    
    int barkAnimationIndex = 1;

    void Awake()
    {
        animator = GetComponent<Animator>();
        // animation = GetComponent<Animation>();
    }
    
    public void Idle()
    {
        // if (animation.IsPlaying("DogIdle")) return;
        // animation.Play("DogIdle");
        animator.SetBool(IsBarking, false);
        animator.SetBool(IsWalking, false);
    }

    public void Bark()
    {
        // if (animation.IsPlaying($"DogBark0{barkAnimationIndex}"))
        // animation.Play($"DogBark0{barkAnimationIndex}");
        
        animator.SetBool(IsBarking, true);
        animator.SetBool(IsWalking, false);
    }

    public void Move()
    {
        // if (animation.IsPlaying("DogWalk")) return;
        // animation.Play("DogWalk");
        
        animator.SetBool(IsBarking, false);
        animator.SetBool(IsWalking, true);
    }

    public void SetBarkAnimation(int barkAnimationIndex)
    {
        this.barkAnimationIndex = Math.Clamp(barkAnimationIndex, 1, 3);
    }
}