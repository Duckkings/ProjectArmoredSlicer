using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerStateController : MonoBehaviour
{
    //用于统一管理玩家的状态
    public State State;
    public static PlayerStateController Instance;
   
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        PlayerController.instance.Init += GoToIdle;
    }
    void Start()
    {
        


    }

    private void GoToIdle()
    {
        StateMachine.Instance.ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
         
        StateMachine.Instance.Update();


    }

   

    //public void PlayerHorizontalBrake()
    //{
    //    PlayerController.instance.CurHorizontalVelocity = Vector3.Lerp(PlayerController.instance.CurHorizontalVelocity, Vector3.zero, Time.deltaTime * 50f);
    //}
}
