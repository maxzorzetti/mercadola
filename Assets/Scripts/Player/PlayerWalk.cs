using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class PlayerWalk : MonoBehaviour
{
    public float speed = 3.0f;
    [Range(0f, 0.25f)]
    public float collisionOffset = 0.05f;
    
    public ContactFilter2D contactFilter;

    [HideInInspector] public Vector2 movement;
    [HideInInspector] public bool isMoving;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Rigidbody2D body;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }
    
    // This _has_ to be called by something every frame
    public void Move(Vector2 direction)
    {
        movement = direction * speed * Time.fixedDeltaTime;
        isMoving = movement != Vector2.zero;
    }

    void FixedUpdate()
    {
        ResolvePlayerMovement();
    }

    void ResolvePlayerMovement()
    {
        if (!isMoving)
        {
            body.position = body.position.Pixelized();
            return;
        }
        
        if (AdvancePlayer(movement)) return;
        if (AdvancePlayer(new Vector2(movement.x, 0))) return;
        if (AdvancePlayer(new Vector2(0, movement.y))) return;
    }

    bool AdvancePlayer(Vector2 movement)
    {
        var hits = body.Cast(movement.normalized, contactFilter, castCollisions, movement.magnitude + collisionOffset);

        if (hits != 0) return false;
        
        body.MovePosition(body.position + movement);
        return true;
    }
}