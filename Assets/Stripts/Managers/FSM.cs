using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    
    public virtual void Enter()
    {
        PlayerStateController.Instance.State = this;
    }
    public virtual void Update()
    {
        
    }
    public abstract void Exit();
}

public class StateMachine
{
    private State curState;
    private static StateMachine instance;

    private StateMachine() { }

    public static StateMachine Instance
    {
        get 
        {
            if (instance == null) { instance = new StateMachine(); }
            return instance;
        }
    }
    public void ChangeState(State state)
    {
        if(curState!=null)
        {
            curState.Exit();
        }

        if(state!=null)
        {
            curState = null;
            curState = state;
            curState.Enter();
        }
    }

    public void Update()
    {
        if(curState!=null) {
            curState.Update();
            }
    }
}

