using UnityEngine;

public class Dog : MonoBehaviour
{
    public float AggroRange = 3;
    public float speed = 3;
    public float ChaseRange = 5;
    public float Patience = 5;
    public float Cooling = 2;
    public float AngerTime = 5;
    public float idleLockTime = 2;

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
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.Update();
    }
    
}
