using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float speed = 3.0f;
    public int pixelsPerUnit = 32;

    public Vector2 movement;
    [HideInInspector] public bool isMoving;

    Vector2 realPosition;
    
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    void Start() {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        realPosition = transform.position;
    }
    
    // This _has_ to be called by something every frame
    public void Move(Vector2 direction)
    {
        movement = direction * speed * Time.deltaTime;
        isMoving = movement != Vector2.zero;
    }

    void Update()
    {
        realPosition += movement;
        UpdatePixelatedPosition();
    }
    
    void UpdatePixelatedPosition()
    {
        var pixelatedPosition = new Vector2(
            Mathf.Round(realPosition.x * pixelsPerUnit) / pixelsPerUnit,
            Mathf.Round(realPosition.y * pixelsPerUnit) / pixelsPerUnit
        );
        transform.position = pixelatedPosition;
    }
}