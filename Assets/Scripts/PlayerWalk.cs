using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float speed = 3.0f;

    public Vector2 movement;
    [HideInInspector] public bool isMoving;
    
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    void Start() {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        transform.Translate(movement);
    }
    
    // This _has_ to be called by something every frame
    public void Move(Vector2 direction)
    {
        movement = direction * speed * Time.deltaTime;
        isMoving = movement != Vector2.zero;
    }
}