using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float speed = 3.0f;
    [HideInInspector]
    public bool isMoving;
    
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    Vector2 movementThisFrame;
    
    void Start() {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void LateUpdate() {
        transform.Translate(movementThisFrame);
        movementThisFrame = Vector2.zero;
        
        UpdateCharacterAnimation();
    }
    
    // This _has_ to be called by something every frame
    public void Move(Vector2 direction) {
        movementThisFrame = direction == Vector2.zero 
            ? Vector2.zero 
            : movementThisFrame + (direction * speed * Time.deltaTime);
        isMoving = movementThisFrame != Vector2.zero;
    }

    void UpdateCharacterAnimation() {
        spriteRenderer.flipX = movementThisFrame.x < 0;
        
        if (isMoving && !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("Exit");
            animator.SetTrigger("RunFront");
        } else if(!isMoving && GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("RunFront");
            animator.SetTrigger("Exit");
        }
    }
}