using UnityEngine;
using UnityEngine.UIElements;

public class MyScript : MonoBehaviour
{
    public Transform characterTransform;
    public float speed = 3.0f;
    bool isMoving;
    Controller controller;


    // Start is called before the first frame update
    void Start() {
        Debug.Log("Start");
        controller = new Controller();
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
            GetComponentInChildren<SpriteRenderer>().flipX = Vector3.left == direction;
        }
    }

    void updateCharacterAnimation() {
        if(isMoving && !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            GetComponentInChildren<Animator>().ResetTrigger("Exit");
            GetComponentInChildren<Animator>().SetTrigger("RunFront");
        } else if(!isMoving && GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CharacterRun")) {
            GetComponentInChildren<Animator>().ResetTrigger("RunFront");
            GetComponentInChildren<Animator>().SetTrigger("Exit");
        }
    }
}