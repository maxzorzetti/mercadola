
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }
    
    bool isDebug;

    public void Initialize(State initialState, bool isDebug = false)
    {
        ChangeState(initialState);
        this.isDebug = isDebug;
    }

    public void ChangeState(State nextState)
    {
        var previousState = CurrentState;
        CurrentState?.ExitState(nextState);
        CurrentState = nextState;
        CurrentState.EnterState(previousState);
        
        if (isDebug) Debug.Log($"Entered state {CurrentState.GetType()}");
    }
    
    public void Update()
    {
        CurrentState.Update();
    }

    public class State
    {
        readonly protected StateMachine stateMachine;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public virtual void ExitState(State nextState)
        {

        }

        public virtual void EnterState(State previousState)
        {

        }

        public virtual void Update()
        {

        }
    }
}