using UnityEngine;

public class Dog : MonoBehaviour
{
    public const float MaxMovementFrequency = 5;
    
    public float speed = 1;
    public float chaseSpeed = 2;
    public float annoyedSpeed = 3;
    public float aggroRange = 3;
    public float chaseRange = 5;
    public float patience = 3;
    public float cooling = 2;
    public float annoyanceDuration = 5;
    public float idleAfterAnnoyanceLockDuration = 2;
    
    [Range(0f, 5f)]
    public float maxMovement = 2;
    [Range(0f, 5f)]
    public float minMovement = 0.5f;
    
    [Range(1f, MaxMovementFrequency)]
    public float movementFrequency = 1;

    internal DogAnimator dogAnimator;
    internal SpriteRenderer sprite;
    internal Follower follower;
    
    StateMachine stateMachine; // = new StateMachine();
    internal DogIdleState idleState;
    internal DogRandomMoveState randomMoveState;
    internal DogAffectionState affectionState;
    internal DogChaseState chaseState;
    internal DogAnnoyedState annoyedState;

    void Start()
    {
        dogAnimator = GetComponentInChildren<DogAnimator>();
        follower = GetComponent<Follower>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        
        stateMachine = new StateMachine();
        idleState = new DogIdleState(this, stateMachine);
        randomMoveState = new DogRandomMoveState(this, stateMachine);
        affectionState = new DogAffectionState(this, stateMachine);
        chaseState = new DogChaseState(this, stateMachine);
        annoyedState = new DogAnnoyedState(this, stateMachine);
        stateMachine.Initialize(idleState, true);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
