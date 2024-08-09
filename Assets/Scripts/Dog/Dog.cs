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
    public float chill = 1;
    public float angerDuration = 5;
    public float idleAfterAnnoyanceLockDuration = 2;

    [Range(0f, 5f)]
    public float maxMovement = 2;
    [Range(0f, 5f)]
    public float minMovement = 0.5f;
    
    [Range(1f, MaxMovementFrequency)]
    public float movementFrequency = 1;
    
    public int annoyanceLevel { get; private set; }
    
    internal DogAnimator dogAnimator;
    internal SpriteRenderer sprite;
    internal Follower follower;
    
    StateMachine stateMachine;
    internal DogIdleState idleState;
    internal DogRandomMoveState randomMoveState;
    internal DogAffectionState affectionState;
    internal DogIrritateState irritateState;
    internal DogChaseState chaseState;
    internal DogAnnoyedState annoyedState;

    void Start()
    {
        dogAnimator = GetComponentInChildren<DogAnimator>();
        follower = GetComponent<Follower>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        
        stateMachine = new StateMachine(gameObject.name, true);
        idleState = new DogIdleState(this, stateMachine);
        randomMoveState = new DogRandomMoveState(this, stateMachine);
        affectionState = new DogAffectionState(this, stateMachine);
        irritateState = new DogIrritateState(this, stateMachine);
        chaseState = new DogChaseState(this, stateMachine);
        annoyedState = new DogAnnoyedState(this, stateMachine);
        stateMachine.OnStateChange += dogAnimator.HandleStateChange;
        stateMachine.Initialize(idleState);
    }

    public void SetAnnoyanceLevel(int level)
    {
        annoyanceLevel = Mathf.Clamp(level, 0, 2);
        
        transform.localScale = Vector3.one * (1 + annoyanceLevel * 0.1f);
        sprite.color = level == 2 ? Color.red : Color.white;
    }

    void Update()
    {
        stateMachine.Update();
    }
}
