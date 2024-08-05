using System;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    public Transform target;
    
    public float stoppingDistance = 1.5f;
    public float maxSpeed = 2f;
    public float acceleration = 10f;
    
    public bool faceTarget = false;

    public UnityEvent OnStartedFollowing;
    public UnityEvent OnReachedStoppingDistance;

    [NonSerialized] public bool isMoving;
    
    Vector3 velocity;
    bool canTriggerStopEvent = true;
    bool canTriggerMoveEvent = true;

    void Start()
    {
        target ??= FindObjectOfType<Dola>().transform;
    }

    void Update()
    {
        if (target == null) return;

        Follow();
        FaceTarget();
    }

    void FaceTarget()
    {
        if (!faceTarget) return;

        var sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.flipX = !(target.position.x < transform.position.x);
    }

    void Follow()
    {
        var position = transform.position;
        var distanceToTarget = Vector2.Distance(position, target.position);

        isMoving = distanceToTarget > stoppingDistance;
        
        if (isMoving)
        {
            MoveTowardsTarget();

            InvokeOnStartedMoving();
        }
        else
        {
            InvokeOnReachedStoppingDistance();
        }
    }
    
    void MoveTowardsTarget()
    {
        var position = transform.position;
        var direction = (target.position - position).normalized;

        velocity += (acceleration * direction) * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    void InvokeOnStartedMoving()
    {
        if (!canTriggerMoveEvent) return;
        
        canTriggerMoveEvent = false;
        canTriggerStopEvent = true;
        OnStartedFollowing.Invoke();
    }
    
    void InvokeOnReachedStoppingDistance()
    {
        if (!canTriggerStopEvent) return;
        
        canTriggerStopEvent = false;
        canTriggerMoveEvent = true;
        OnReachedStoppingDistance.Invoke();
    }
}
