using System;
using UnityEngine;

[RequireComponent(typeof(Follower))]
public class EyeMaw : MonoBehaviour
{
    [Range(0f, 5f)]
    public float initialSpeed = 2f;
    [Range(0f, 1f)]
    public float speedupPerSecond = 0.025f;
    public float attackLockoutDuration = 2f;
    
    [Range(0f, 1f)]
    public float biteRange = 1f;

    public HitBox biteHitBox;

    Animator animator;
    Follower follower;
    AudioSource audioSource;
    
    Tentacle mainTentacle;
    Transform hitBoxPoint;

    bool isAttackLockedOut;
    Progression attackLockoutTimer;
    float originalWiggleSpeed;
    static readonly int Bite = Animator.StringToHash("Bite");

    void Start()
    {
        animator = GetComponent<Animator>();
        follower = GetComponent<Follower>();
        audioSource = GetComponent<AudioSource>();

        mainTentacle = transform.Find("Tentacles/TentacleMain").GetComponent<Tentacle>();
        originalWiggleSpeed = mainTentacle.wiggleSpeed;
        hitBoxPoint = transform.Find("HitBoxPoint");
        
        follower.maxSpeed = initialSpeed;
        attackLockoutTimer = new Progression(attackLockoutDuration);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isAttackLockedOut)
        {
            follower.maxSpeed += speedupPerSecond * Time.deltaTime;
            
            if (follower.isWithinStoppingDistance && !animator.GetCurrentAnimatorStateInfo(0).IsName("Bite"))
            {
                animator.SetTrigger(Bite);
            }
            
            return;
        }

        if (attackLockoutTimer.AdvanceAndConsume())
        {
            isAttackLockedOut = false;
            ToggleLockoutVisuals(false);
            follower.followTarget = true;
        }
    }

    void ToggleLockoutVisuals(bool isLockedOut)
    {
        if (isLockedOut)
        {
            animator.speed = 0.1f;
            follower.faceTarget = false;
            mainTentacle.wiggleSpeed = 5;
        }
        else
        {
            animator.speed = 1;
            follower.faceTarget = true;
            mainTentacle.wiggleSpeed = originalWiggleSpeed;
        }
    }
    
    public void HandleOnEyeMawAttackAnimationEvent()
    {
        var bite = Instantiate(biteHitBox, transform);
        bite.transform.position = hitBoxPoint.position;
        
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void HandleOnEyeMawHit(Hit hit)
    {
        if (!enabled) return;
        
        // Check if the hit event is from _this_ eyemaw
        if (hit.hitBox.transform.parent.gameObject != gameObject)
        {
            return;
        }
        
        Debug.Log($"{name}: nomnomnom");
        
        isAttackLockedOut = true;
        ToggleLockoutVisuals(true);
        attackLockoutTimer.Reset();
        
        follower.maxSpeed = initialSpeed;
        follower.followTarget = false;
    }
}
