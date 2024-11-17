using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeState : State
{
    // Start is called before the first frame update
    public override void Enter()
    {
        Debug.Log("½øÈëÉ²³µ×´Ì¬");
        base.Enter();
        //PlayerController.instance.TargetVelocity = Vector3.zero;
    }
    public override void Exit()
    {
       
    }

    // Update is called once per frame
    public override void Update()
    {
        PlayerController.instance.NormalMovement();
        base.Update();

        if (InputManager.instance.MoveInput.magnitude != 0)
        {
            StateMachine.Instance.ChangeState(new MotionState());
        }

        if (InputManager.instance.JumpInput.magnitude != 0)
        {
            StateMachine.Instance.ChangeState(new ReadyJumpState());
        }

        if (PlayerController.instance.CurVelocity.magnitude < 1)
        {
            StateMachine.Instance.ChangeState(new IdleState());
        }

        else if (InputManager.instance.DashInput)
        {
            StateMachine.Instance.ChangeState(new ReadyDashState());
        }

    }
}
