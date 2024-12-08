using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerStateController : MonoBehaviour
{
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
    
    }
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
         
        StateMachine.Instance.Update();


    }

   
}
