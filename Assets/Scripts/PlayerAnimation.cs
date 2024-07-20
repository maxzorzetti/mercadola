using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerWalk playerWalk;
    SpriteRenderer sprite;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerWalk = GetComponent<PlayerWalk>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        UpdateCharacterAnimation();
    }
    
    void UpdateCharacterAnimation() {
        sprite.flipX = playerWalk.movement.x < 0;
        
        if (playerWalk.isMoving && !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("Exit");
            animator.SetTrigger("RunFront");
        } else if(!playerWalk.isMoving && GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("RunFront");
            animator.SetTrigger("Exit");
        }
    }
}