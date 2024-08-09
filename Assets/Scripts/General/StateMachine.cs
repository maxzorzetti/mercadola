using System;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }
    
    public Action<State> OnStateChange;
    
    string name;
    bool isDebug;
    
    public StateMachine(string name = null, bool isDebug = false) 
    {
        this.name = name;
        this.isDebug = isDebug;
    }

    public void Initialize(State initialState)
    {
        ChangeState(initialState);
    }

    public void ChangeState(State nextState)
    {
        var previousState = CurrentState;
        CurrentState?.ExitState(nextState);
        CurrentState = nextState;
        CurrentState.EnterState(previousState);
        
        if (isDebug) Debug.Log($"{name ?? GetType().ToString()} entered state {CurrentState.GetType()}");
        
        OnStateChange?.Invoke(CurrentState);
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