using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public override void Enter()
    {
        Debug.Log("Dash");
        base.Enter();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        PlayerController.instance.NormalMovement();
        if (!InputManager.instance.DashInput)
        {
            StateMachine.Instance.ChangeState(new MotionState());
        }
        if (InputManager.instance.JumpInput.magnitude != 0)
        {
            StateMachine.Instance.ChangeState(new ReadyJumpState());
        }
        else if (InputManager.instance.MoveInput.magnitude == 0)
        {
            StateMachine.Instance.ChangeState(new BrakeState());
        }
        else if (PlayerController.instance.CurVelocity.magnitude < 0.2f && InputManager.instance.MoveInput.magnitude == 0)
        {
            StateMachine.Instance.ChangeState(new IdleState());
        }
        base.Update();
    }
}
