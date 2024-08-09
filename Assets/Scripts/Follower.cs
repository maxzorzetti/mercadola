using System;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    public enum TargetFacing
    {
        Flip, Rotate
    }
    
    public Transform target;
    
    public float followRange = 5f;
    public float stoppingDistance = 1.5f;
    public float maxSpeed = 2f;
    public float acceleration = 10f;
    public float deacceleration = 30;

    public bool followTarget = true;
    public bool faceTarget = false;
    public TargetFacing facingType;
    public int rotationSpeed = 10;

    public State state { get; private set; }
    public bool isWithinFollowRange { get; private set; }
    public bool isWithinStoppingDistance { get; private set; }

    public UnityEvent OnStartedFollowing;
    public UnityEvent OnReachedStoppingDistance;
    
    Vector2 velocity;
    bool canTriggerStopEvent = true;
    bool canTriggerMoveEvent = true;

    void Start()
    {
        target ??= FindObjectOfType<Dola>().transform;
    }

    void Update()
    {
        Follow();
        FaceTarget();
        UpdateState();
    }

    void FaceTarget()
    {
        if (!faceTarget || target == null) return;

        switch (facingType)
        {
            case TargetFacing.Flip:
                var sprite = GetComponentInChildren<SpriteRenderer>();
                sprite.flipX = (target.position.x < transform.position.x);
                break;
            case TargetFacing.Rotate:
                var direction = target.position - transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                break;
        }
    }

    void Follow()
    {
        if (target == null) return;
        
        var position = transform.position;
        var distanceToTarget = Vector2.Distance(position, target.position);
        
        isWithinFollowRange = distanceToTarget < followRange;
        isWithinStoppingDistance = distanceToTarget < stoppingDistance;

        if (!followTarget || !isWithinFollowRange)
        {
            Decelerate();
            return;
        }

        if (isWithinStoppingDistance)
        {
            Decelerate();
            InvokeOnReachedStoppingDistance();
            return;
        }
        
        MoveTowardsTarget();
        InvokeOnStartedMoving();
    }
    
    void MoveTowardsTarget()
    {
        var position = (Vector2)transform.position;
        var direction = ((Vector2)target.position - position).normalized;

        velocity += (acceleration * direction) * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    void Decelerate()
    {
        var oppositeDirection = velocity.normalized * -1;
        var velocityReduction = deacceleration * oppositeDirection * Time.deltaTime;
        velocity += Vector2.ClampMagnitude(velocityReduction, velocity.magnitude);
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
    
    void UpdateState()
    {
        switch (isWithinFollowRange, isWithinStoppingDistance)
        {
            case (true, true):
                state = State.ReachedTarget;
                break;
            case (true, false):
                state = State.MovingTowardsTarget;
                break;
            case (false, _):
                state = followTarget ? State.OutsideRange : State.Idle;
                break;
        }
    }

    public enum State
    {
        Idle, OutsideRange, MovingTowardsTarget, ReachedTarget
    }
}
