using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBrakeState : State
{
    public override void Enter()
    {
       
        Debug.Log("Enter AirMotion State");
        base.Enter();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        PlayerController.instance.NormalMovement();


        base.Update();

        if (PlayerController.instance.isGrounded)
        {
            if (PlayerController.instance.CurHorizontalVelocity.magnitude < 0.2f)
            {
                StateMachine.Instance.ChangeState(new IdleState());
            }
            if (InputManager.instance.MoveInput.magnitude == 0)
            {
                StateMachine.Instance.ChangeState(new BrakeState());
            }
            if (InputManager.instance.MoveInput.magnitude != 0)
            {
                StateMachine.Instance.ChangeState(new MotionState());
            }

        }
        else if (InputManager.instance.DashInput)
        {
            StateMachine.Instance.ChangeState(new ReadyDashState());
        }







    }
}
