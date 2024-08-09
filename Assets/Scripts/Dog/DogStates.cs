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
        var period = Dog.MaxMovementFrequency - (dog.movementFrequency);
        
        return new Progression(Random.Range(period * 0.5f, period * 1.5f));
    }
}

public class DogRandomMoveState : DogState
{
    Vector3 randomMovementTarget;
    
    public DogRandomMoveState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }

    public override void EnterState(StateMachine.State previousState)
    {
        dog.follower.faceTarget = false;
        randomMovementTarget = ((Vector2)dog.transform.position + GetRandomDistance()).Pixelized();
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
    
    Vector2 GetRandomDistance()
    {
        var direction = Random.insideUnitCircle.normalized;

        return direction * Random.Range(dog.minMovement, dog.maxMovement);
    }
}


public class DogChaseState : DogState
{
    public DogChaseState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }
    
    public override void EnterState(StateMachine.State previousState)
    {
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

    Progression chillProgression = new Progression();
    
    public override void EnterState(StateMachine.State previousState)
    {
        chillProgression = new Progression(dog.chill);
        dog.follower.followTarget = true;
        dog.follower.faceTarget = true;
        dog.SetAnnoyanceLevel(0);
    }

    public override void Update()
    {
        if (chillProgression.AdvanceAndConsume())
        {
            dog.SetAnnoyanceLevel(dog.annoyanceLevel - 1);
            chillProgression.Reset();
        }

        if (!dog.sprite.AreFacingEachOther(dog.follower.target.GetComponentInChildren<SpriteRenderer>()))
        {
            stateMachine.ChangeState(dog.irritateState);
        }
        else if (dog.follower.state == Follower.State.MovingTowardsTarget)
        {
            stateMachine.ChangeState(dog.chaseState);
        }
    }
}

public class DogIrritateState : DogState
{
    public DogIrritateState(Dog dog, StateMachine stateMachine) : base(dog, stateMachine) { }

    Progression irritationProgression = new Progression();
    
    public override void EnterState(StateMachine.State previousState)
    {
        irritationProgression = new Progression(dog.patience);
        dog.follower.followTarget = true;
        dog.follower.faceTarget = true;
    }

    public override void Update()
    {
        if (irritationProgression.AdvanceAndConsume())
        {
            dog.SetAnnoyanceLevel(dog.annoyanceLevel + 1);
        }
        
        if (dog.sprite.AreFacingEachOther(dog.follower.target.GetComponentInChildren<SpriteRenderer>()))
        {
            stateMachine.ChangeState(dog.affectionState);
        } 
        else if (dog.follower.state == Follower.State.MovingTowardsTarget)
        {
            stateMachine.ChangeState(dog.chaseState);
        } 
        else if (dog.annoyanceLevel == 2)
        {
            stateMachine.ChangeState(dog.annoyedState);
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
        angerProgression = new Progression(dog.angerDuration);
        
        previousStoppingDistance = dog.follower.stoppingDistance;
        dog.follower.followTarget = true;
        dog.follower.faceTarget = true;
        dog.follower.maxSpeed = dog.annoyedSpeed;
        dog.follower.stoppingDistance = 0;
    }

    public override void ExitState(StateMachine.State nextState)
    {
        dog.follower.stoppingDistance = previousStoppingDistance;
        dog.SetAnnoyanceLevel(0);
    }

    public override void Update()
    {
        if (angerProgression.AdvanceAndConsume())
        {
            stateMachine.ChangeState(dog.idleState);
        }
    }
}