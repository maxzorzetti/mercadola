using UnityEngine;

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
        if(controller.IsPressingAnyKeys()) {
            if(controller.Up.IsPressed()) {
                move(Vector3.up);
            } 
            if(controller.Left.IsPressed()) {
                move(Vector3.left);
            } 
            if(controller.Down.IsPressed()) {
                move(Vector3.down);
            }
            if(controller.Right.IsPressed()) {
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
        GetComponentInChildren<Animator>().SetBool("Run", isMoving);
    }
}