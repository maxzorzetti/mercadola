using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DogAnimations : MonoBehaviour
{
    static readonly int IsBarking = Animator.StringToHash("isBarking");
    static readonly int IsWalking = Animator.StringToHash("isWalking");
    
    Animator animator;
    // Animation animation;
    
    int barkAnimationIndex = 1;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Idle()
    {
        animator.SetBool(IsBarking, false);
        animator.SetBool(IsWalking, false);
    }

    public void Bark()
    {
        animator.SetBool(IsBarking, true);
        animator.SetBool(IsWalking, false);
    }

    public void Move()
    {
        animator.SetBool(IsBarking, false);
        animator.SetBool(IsWalking, true);
    }

    public void SetBarkAnimation(int barkAnimationIndex)
    {
        this.barkAnimationIndex = Math.Clamp(barkAnimationIndex, 1, 3);
    }
    
    public void HandleStateChange(StateMachine.State state)
    {
        switch (state)
        {
            case DogIdleState newState:
                Idle();
                break;
            case DogRandomMoveState newState:
                Move();
                break;
            case DogChaseState newState:
                Move();
                break;
            case DogAffectionState newState:
                Idle();
                break;
            case DogIrritateState newState:
                Bark();
                break;
            case DogAnnoyedState newState:
                Bark();
                break;
        }
    }
}