using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyJumpState : State
{
    public override void Enter()
    {
       
        Debug.Log("Ready Jump");
        base.Enter();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {

        PlayerController.instance.NormalMovement();
        base.Update();
        StateMachine.Instance.ChangeState(new JumpState());






    }
}
