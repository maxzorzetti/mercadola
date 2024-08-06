using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class DogState : StateMachine.State
{
    protected Dog dog;
    
    public DogState(Dog dog, StateMachine stateMachine) : base(stateMachine)
    {
        this.dog = dog;
    }
}

public class DogIdleState : DogState
{
    public DogIdleState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }

    Progression idleLockTimer;
    bool canLeaveIdle;
    
    Progression randomMovementTimer;

    public override void EnterState(StateMachine.State previousState)
    {
        dog.dogAnimator.Idle();
        dog.follower.faceTarget = false;
        dog.follower.followTarget = false;
        dog.follower.followRange = dog.aggroRange;
        
        randomMovementTimer = NewRandomMovementTimer();
        
        if (previousState is not DogRandomMoveState)
        {
            canLeaveIdle = false;
            idleLockTimer = new Progression(previousState is DogAnnoyedState ? dog.idleAfterAnnoyanceLockDuration : 0.5f);
        }
    }
    
    public override void Update()
    {
        if (idleLockTimer.AdvanceAndConsume(Time.deltaTime))
        {
            canLeaveIdle = true;
        }
        
        if (!canLeaveIdle) return;

        if (dog.follower.isWithinFollowRange)
        {
            stateMachine.ChangeState(dog.chaseState);
            return;
        }
        
        if (randomMovementTimer.AdvanceAndConsume(Time.deltaTime))
        {
            stateMachine.ChangeState(dog.randomMoveState);
            return;
        }
    }
    
    Progression NewRandomMovementTimer()
    {
        return new Progression(Random.Range(0.5f, 1.5f));
    }
}

public class DogRandomMoveState : DogState
{
    Vector3 randomMovementTarget;
    
    public DogRandomMoveState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }

    public override void EnterState(StateMachine.State previousState)
    {
        dog.dogAnimator.Move();
        dog.follower.faceTarget = false;
        randomMovementTarget = (dog.transform.position + GetRandomDistance()).Pixelized();
    }

    public override void Update()
    {
        DoMovement();
        
        if (dog.follower.isWithinFollowRange)
        {
            stateMachine.ChangeState(dog.chaseState);
        }
    }
    
    void DoMovement()
    {
        var transform = dog.transform;
        var position = transform.position;
        var direction = (randomMovementTarget - position).normalized;
        var newPosition = position + dog.speed * direction * Time.deltaTime;
        
        transform.position = newPosition;
        dog.sprite.flipX = direction.x < 0;
        
        if ((newPosition - randomMovementTarget).sqrMagnitude < 0.1f)
        {
            stateMachine.ChangeState(dog.idleState);
        }
    }
    
    Vector3 GetRandomDistance()
    {
        var direction = Random.insideUnitCircle;

        var maxDist = Vector2.one * dog.maxMovement;
        var minDist = Vector2.one * dog.minMovement;
        
        return new Vector3(
            Mathf.Lerp(minDist.x, maxDist.x, direction.x),
            Mathf.Lerp(minDist.y, maxDist.y, direction.y)
        );
    }
}


public class DogChaseState : DogState
{
    public DogChaseState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }
    
    public override void EnterState(StateMachine.State previousState)
    {
        dog.dogAnimator.Move();
        dog.follower.followTarget = true;
        dog.follower.faceTarget = true;
        dog.follower.maxSpeed = dog.chaseSpeed;
        dog.follower.followRange = dog.chaseRange;
    }

    public override void ExitState(StateMachine.State nextState)
    {
        dog.follower.followTarget = false;
    }
    
    public override void Update()
    {
        switch (dog.follower.state)
        {
            case Follower.State.MovingTowardsTarget:
                break;
            case Follower.State.ReachedTarget:
                stateMachine.ChangeState(dog.affectionState);
                break;
            default:
                stateMachine.ChangeState(dog.idleState);
                break;
        }
    }
}

public class DogAffectionState : DogState
{
    public DogAffectionState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }

    int annoyanceLevel;
    Progression annoyanceProgression = new Progression();
    Progression chillProgression = new Progression();
    
    public override void EnterState(StateMachine.State previousState)
    {
        annoyanceProgression = new Progression(dog.patience);
        chillProgression = new Progression(dog.cooling);
        dog.dogAnimator.Idle();
        dog.follower.followTarget = true;
        dog.follower.faceTarget = true;
        SetAnnoyanceLevel(0);
    }
    
    public void SetAnnoyanceLevel(int level)
    {
        annoyanceLevel = Mathf.Clamp(level, 0, 2);
        dog.dogAnimator.SetBarkAnimation(annoyanceLevel + 1);
        dog.transform.localScale = Vector3.one * (1 + annoyanceLevel * 0.15f);
        
        if (annoyanceLevel == 2)
        {
            stateMachine.ChangeState(dog.annoyedState);
        }
    }

    public override void Update()
    {
        UpdateAnnoyance();
        
        if (dog.follower.state == Follower.State.MovingTowardsTarget)
        {
            stateMachine.ChangeState(dog.chaseState);
        }
    }

    void UpdateAnnoyance()
    {
        if (dog.sprite.AreFacingEachOther(dog.follower.target.GetComponentInChildren<SpriteRenderer>()))
        {
            dog.dogAnimator.Idle();
            if (chillProgression.AdvanceAndConsume(dog.cooling * Time.deltaTime))
            {
                SetAnnoyanceLevel(annoyanceLevel - 1);
            }
        }
        else
        {
            if (annoyanceProgression.AdvanceAndConsume(Time.deltaTime))
            {
                SetAnnoyanceLevel(annoyanceLevel + 1);
            }
            
            switch (annoyanceLevel)
            {
                default:
                    dog.dogAnimator.Bark();
                    break;
            }
        }
    }
}

public class DogAnnoyedState : DogState
{
    Progression angerProgression;
    float previousStoppingDistance;
    
    public DogAnnoyedState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }
    
    public override void EnterState(StateMachine.State previousState)
    {
        angerProgression = new Progression(dog.annoyanceDuration);
        dog.dogAnimator.Bark();
        dog.sprite.color = Color.red;
        
        previousStoppingDistance = dog.follower.stoppingDistance;
        dog.follower.followTarget = true;
        dog.follower.faceTarget = true;
        dog.follower.maxSpeed = dog.annoyedSpeed;
        dog.follower.stoppingDistance = 0;
    }

    public override void ExitState(StateMachine.State nextState)
    {
        dog.sprite.color = Color.white;
        dog.follower.stoppingDistance = previousStoppingDistance;
        dog.affectionState.SetAnnoyanceLevel(0);
    }

    public override void Update()
    {
        if (angerProgression.AdvanceAndConsume(Time.deltaTime))
        {
            stateMachine.ChangeState(dog.idleState);
        }
    }
}