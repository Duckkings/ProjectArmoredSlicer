using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public override void Enter()
    {
       
        Debug.Log("Jump");
        base.Enter();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        PlayerController.instance.BurstJumpBoost();
        
        


        base.Update();
        StateMachine.Instance.ChangeState(new AirMotionState());






    }
}
