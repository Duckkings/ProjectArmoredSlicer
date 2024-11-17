using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        Debug.Log("Enter Idle State");
        base.Enter();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        PlayerController.instance.NormalMovement();

        base.Update();
        if(InputManager.instance.MoveInput.magnitude != 0)
        {
            StateMachine.Instance.ChangeState(new MotionState());
        }

        if (InputManager.instance.JumpInput.magnitude != 0)
        {
            StateMachine.Instance.ChangeState(new ReadyJumpState());
        }


    }
}
