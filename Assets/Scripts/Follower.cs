using Extensions;
using UnityEngine;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    public Transform target;
    
    public float stoppingDistance = 1.5f;
    public float maxSpeed = 2f;
    public float acceleration = 10f;

    public UnityEvent OnStartedMoving;
    public UnityEvent OnReachedStoppingDistance;

    Vector3 velocity;
    bool canTriggerStopEvent = true;
    bool canTriggerMoveEvent = true;

    void Start()
    {
        target ??= FindObjectOfType<Dola>().transform;
    }

    void Update()
    {
        var position = transform.position;
        var distanceToTarget = Vector2.Distance(position, target.position);
        
        if (distanceToTarget < stoppingDistance)
        {
            transform.position = position.Pixelized();

            if (canTriggerStopEvent)
            {
                canTriggerStopEvent = false;
                canTriggerMoveEvent = true;
                OnReachedStoppingDistance.Invoke();
            }
        }
        else
        {
            MoveTowardsTarget();

            if (canTriggerMoveEvent)
            {
                canTriggerMoveEvent = false;
                canTriggerStopEvent = true;
                OnStartedMoving.Invoke();
            }
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
}
