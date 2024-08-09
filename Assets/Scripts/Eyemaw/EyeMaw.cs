using UnityEngine;

[RequireComponent(typeof(Follower))]
public class EyeMaw : MonoBehaviour
{
    [Range(0f, 5f)]
    public float initialSpeed = 2f;
    [Range(0f, 1f)]
    public float speedupPerSecond = 0.025f;
    public float attackLockoutDuration = 2f;

    HitBox hitBox;
    Animator animator;
    Follower follower;
    RotateToTarget rotateToTarget;
    Tentacle mainTentacle;

    bool isAttackLockedOut;
    Progression attackLockoutTimer;
    float originalWiggleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<HitBox>();
        animator = GetComponent<Animator>();
        follower = GetComponent<Follower>();
        rotateToTarget = GetComponent<RotateToTarget>();
        mainTentacle = transform.Find("Tentacles/TentacleMain").GetComponent<Tentacle>();
        originalWiggleSpeed = mainTentacle.wiggleSpeed;
        
        follower.maxSpeed = initialSpeed;
        attackLockoutTimer = new Progression(attackLockoutDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttackLockedOut)
        {
            follower.maxSpeed += speedupPerSecond * Time.deltaTime;
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
            rotateToTarget.enabled = false;
            mainTentacle.wiggleSpeed = 5;
        }
        else
        {
            animator.speed = 1;
            rotateToTarget.enabled = true;
            mainTentacle.wiggleSpeed = originalWiggleSpeed;
        }
    }
    
    public void HandleOnEyeMawAttackAnimationEvent()
    {

    }

    public void HandleOnEyeMawHit(Hit hit)
    {
        if (hit.hitBox.gameObject != gameObject)
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
