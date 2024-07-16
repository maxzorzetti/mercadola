using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    float pressDuration;
    public Transform characterTransform;
    public float speed = 3.0f;
    bool isMoving;


    // Start is called before the first frame update
    void Start() {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update() {
        getUserInput();
        updateCharacterAnimation();
    }

    void getUserInput() {
        if(
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow)
        ) {
            if(Input.GetKey(KeyCode.RightArrow)) {
                move(Vector3.right);
            } 
            if(Input.GetKey(KeyCode.LeftArrow)) {
                move(Vector3.left);
            } 
            if(Input.GetKey(KeyCode.UpArrow)) {
                move(Vector3.up);
            } 
            if(Input.GetKey(KeyCode.DownArrow)) {
                move(Vector3.down);
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
