using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirReadyDashState : State
{
    public override void Enter()
    {
       
        Debug.Log("AirReadyDash");
        base.Enter();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {

        PlayerController.instance.NormalMovement();
        base.Update();
        StateMachine.Instance.ChangeState(new AirDashState());






    }
}
