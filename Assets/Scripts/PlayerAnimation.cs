using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;

    PlayerWalk playerWalk;
    PlayerAction action;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerWalk = GetComponent<PlayerWalk>();
        action = GetComponent<PlayerAction>();

        action.OnInteract += OnInteract;
        action.OnInvestigate += OnInvestigate;
        action.OnAmaze += OnAmaze;
    }
    
    public void OnInteract() {
        GetComponentInChildren<Spinner>().Spin();
    }

    public void OnInvestigate() {
        animator.SetBool("isMoving", false);
        animator.SetBool("isInvestigating", true);
        animator.SetBool("isAmazed", false);
        animator.SetBool("isFacingBack", false);
    }
    
    public void OnAmaze() {
        animator.SetBool("isMoving", false);
        animator.SetBool("isInvestigating", false);
        animator.SetBool("isAmazed", true);
        animator.SetBool("isFacingBack", false);
    }
    
    void Update()
    {
        UpdateMovementAnimation();
    }
    
    void UpdateMovementAnimation()
    {
        animator.SetBool("isMoving", playerWalk.isMoving);
        animator.SetBool("isInvestigating", false);
        animator.SetBool("isAmazed", false);

        // Only bother to flip the sprite horizontally if the player is moving horizontally
        if (playerWalk.isMoving && playerWalk.movement.x != 0)
        {
            sprite.flipX = playerWalk.movement.x < 0;
        }
        if (playerWalk.isMoving && playerWalk.movement.y != 0)
        {
            animator.SetBool("isFacingBack", playerWalk.movement.y > 0);
        }
    }
}