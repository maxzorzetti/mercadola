using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DogAnimator : MonoBehaviour
{
    static readonly int IsBarking = Animator.StringToHash("isBarking");
    static readonly int IsWalking = Animator.StringToHash("isWalking");
    
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Stop()
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

    public void SetBarkSprite(int barkSpriteIndex)
    {
        // Do something
    }
}