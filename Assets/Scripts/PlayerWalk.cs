using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float speed = 3.0f;
    bool isMoving;
    Controller controller;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        controller = new Controller();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        getUserInput();
        updateCharacterAnimation();
    }

    void getUserInput() {
        if(controller.IsHoldingMovementKeys()) {
            if(controller.Up.IsHold()) {
                move(Vector3.up);
            } 
            if(controller.Left.IsHold()) {
                move(Vector3.left);
            } 
            if(controller.Down.IsHold()) {
                move(Vector3.down);
            }
            if(controller.Right.IsHold()) {
                move(Vector3.right);
            } 
        } else { 
            isMoving = false;
        }
    }

    void move(Vector3 direction) {
        isMoving = true;
        transform.Translate(direction * speed * Time.deltaTime);
        if(Vector3.left == direction || Vector3.right == direction) {
            spriteRenderer.flipX = Vector3.left == direction;
        }
    }

    void updateCharacterAnimation() {
        if(isMoving && !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("Exit");
            animator.SetTrigger("RunFront");
        } else if(!isMoving && GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            animator.ResetTrigger("RunFront");
            animator.SetTrigger("Exit");
        }
    }
}