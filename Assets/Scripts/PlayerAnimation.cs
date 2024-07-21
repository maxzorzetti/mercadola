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
    }
    
    public void OnInteract() {
        GetComponentInChildren<Spinner>().Spin();
    }

    public void OnInvestigate() {
        UpdateInvestigateAnimation();
    }
    
    public void OnAmaze() {
        UpdateAmazeAnimation();
    }
    
    void Update()
    {
        UpdateInvestigateAnimation();
        UpdateAmazeAnimation();
        UpdateMovementAnimation();
    }
    
    void UpdateMovementAnimation()
    {
        animator.SetBool("isMoving", playerWalk.isMoving);

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

    void UpdateInvestigateAnimation() {
        animator.SetBool("isInvestigating", action.isInvestigating);

        if(action.isInvestigating) {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAmazed", false);
            animator.SetBool("isFacingBack", false);
        }
    }

    void UpdateAmazeAnimation() {

    }
}