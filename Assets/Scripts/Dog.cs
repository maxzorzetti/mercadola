using System;
using UnityEngine;

public class Dog : MonoBehaviour
{
    Animator animator;
    SpriteRenderer renderer;
    Follower follower;
    
    static readonly int IsBarking = Animator.StringToHash("isBarking");
    static readonly int IsWalking = Animator.StringToHash("isWalking");

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        follower = GetComponent<Follower>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        Stop();
    }

    void Update()
    {
        renderer.flipX = follower.target.position.x > transform.position.x;
    }

    public void Stop()
    {
        animator.SetBool(IsBarking, false);
        animator.SetBool(IsWalking, false);
    }

    public void Bark()
    {
        Debug.Log($"{name}: Woof!");
        animator.SetBool(IsBarking, true);
        animator.SetBool(IsWalking, false);
    }
    
    public void Move()
    {
        animator.SetBool(IsBarking, false);
        animator.SetBool(IsWalking, true);
    }
}
