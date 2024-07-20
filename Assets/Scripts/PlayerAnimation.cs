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
        animator.SetTrigger("Investigate");
    }
    
    public void OnAmaze() {
        animator.SetTrigger("Amazed");
    }
    
    void Update()
    {
        UpdateMovementAnimation();
    }
    
    void UpdateMovementAnimation()
    {
        // Only bother to flip the sprite horizontally if the player is moving horizontally
        if (playerWalk.isMoving && playerWalk.movement.x != 0)
        {
            sprite.flipX = playerWalk.movement.x < 0;
        }
        
        // TODO: Figure out how to use Animator state machines lol
        if (playerWalk.isMoving && !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("Exit");
            animator.SetTrigger("RunFront");
        } else if(!playerWalk.isMoving && GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("RunFront");
            animator.SetTrigger("Exit");
        }
    }
}