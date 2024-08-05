using UnityEngine;

public class Dog : MonoBehaviour
{
    public float Patience = 5;
    public float Cooling = 2;
    public float AngerTime = 5;
    
    DogAnimator dogAnimator;
    SpriteRenderer sprite;
    Follower follower;
    
    DogState state = DogState.Idle;

    int annoyanceLevel;
    Progression annoyanceProgression = new Progression();
    Progression chillProgression = new Progression();
    Progression angerProgression;

    void Awake()
    {
        dogAnimator = GetComponentInChildren<DogAnimator>();
        follower = GetComponent<Follower>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    void Start()
    {
        angerProgression = new Progression(AngerTime);
    }
    
    public void HandleOnStartedFollowing()
    {
        SwitchState(DogState.Follow);
    }
    
    public void HandleOnReachedStoppingDistance()
    {
        SwitchState(DogState.Idle);
    }

    void Idle()
    {
        DoAnnoyance();
    }

    void DoAnnoyance()
    {
        if (sprite.AreFacingEachOther(follower.target.GetComponentInChildren<SpriteRenderer>()))
        {
            dogAnimator.Stop();
            if (chillProgression.AdvanceAndConsume(Cooling * Time.deltaTime))
            {
                IncrementAnnoyanceLevel(-1);
            }
        }
        else
        {
            if (annoyanceProgression.AdvanceAndConsume(Time.deltaTime / Patience))
            {
                IncrementAnnoyanceLevel(1);
            }
            
            switch (annoyanceLevel)
            {
                default:
                    dogAnimator.Bark();
                    break;
            }
        }
    }
    
    void IncrementAnnoyanceLevel(int amount)
    {
        annoyanceLevel = Mathf.Clamp(annoyanceLevel + amount, 0, 2);
        dogAnimator.SetBarkSprite(annoyanceLevel);
        
        if (annoyanceLevel == 2)
        {
            SwitchState(DogState.Angry);
        }
    }
    
    void Follow()
    {
        dogAnimator.Move();
    }
    
    void Angry()
    {
        sprite.color = Color.red;
        
        if (angerProgression.AdvanceAndConsume(Time.deltaTime))
        {
            angerProgression.Reset();
            
            LeaveAngryState();
        }
    }

    void LeaveAngryState()
    {
        sprite.color = Color.white;
        state = follower.isMoving ? DogState.Follow : DogState.Idle;
    }
    
    void Update()
    {
        switch (state)
        {
            case DogState.Idle: 
                Idle(); 
                break;
            case DogState.Follow:
                Follow();
                break;
            case DogState.Angry:
                Angry();
                break;
        }
    }

    void SwitchState(DogState newState)
    {
        switch (state, newState)
        {
            case (DogState.Idle, _):
                state = newState;
                break;
            case (DogState.Follow, _):
                state = newState;
                break;
            case (DogState.Angry, _):
                // Do not change
                break;
        }
    }

    enum DogState
    {
        Idle,
        Follow,
        Angry
    }
}
