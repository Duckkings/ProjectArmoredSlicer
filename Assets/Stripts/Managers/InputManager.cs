using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    public PlayerControl InputActions;
    public Vector3 MoveInput { get; private set; }

    
    public Vector3 JumpInput { get; private set; }

    public Vector3 CameraMove;

    public bool DashInput;
    public bool IsUsingGamepad { get; private set; }

    private bool allowInput = false;





    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        InputActions= new PlayerControl();
        InputActions.Enable();
        

       

    }
    void Start()
    {
        MoveInput = Vector3.zero;
        CameraMove= Vector3.zero;
        JumpInput = Vector3.zero;
        DashInput = false;
        PlayerController.instance.Init += AllowInput;
        IsUsingGamepad = false;
        
        InputActions.Player.Jump.performed += OnJumpPerformed;
        InputActions.Player.Jump.canceled += OnJumpCanceled;
        InputActions.Player.Dash.performed += OnDashPerformed;
        InputActions.Player.Dash.canceled += OnDashCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (allowInput)
        {
            Input();
        }


    }

    private void AllowInput()
    {
        allowInput = true;
    }

    private void Input()
    {
        Vector2 input = InputActions.Player.Move.ReadValue<Vector2>();
        Vector2 Camerainput = InputActions.Player.Camera.ReadValue<Vector2>();
        Vector3 TargetMoveInput = new Vector3(input.x, 0f, input.y);



        if (Mathf.Abs(TargetMoveInput.x) > 0.1f || Mathf.Abs(TargetMoveInput.z) > 0.1f)
        {
            MoveInput = TargetMoveInput /*Vector3.Lerp(MoveInput, TargetMoveInput, Time.deltaTime * SmoothTime)*/;
        }
        else
        {
            MoveInput = Vector3.zero;
        }

        if (Mathf.Abs(Camerainput.x) > 0.1f || Mathf.Abs(Camerainput.y) > 0.1f)
        {
            CameraMove = Camerainput;
            //Debug.Log(CameraMove);
        }
        else
        {
            CameraMove = Vector2.zero;
        }
    }

    void OnDestory()
    {
        InputActions.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpInput = new Vector3(0f, 1f, 0f);
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpInput = Vector3.zero;
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        DashInput = true;
    }

    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        DashInput = false;
    }

}
