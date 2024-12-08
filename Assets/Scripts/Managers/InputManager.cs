using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    public PlayerControl InputActions;
    public bool IsUsingGamepad { get; private set; }

    public float MovementSmoothTime;

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
        
        InputActions.Player.Jump.performed += OnJumpPerformed;
        InputActions.Player.Jump.canceled += OnJumpCanceled;
        InputActions.Player.Dash.performed += OnDashPerformed;
        InputActions.Player.Dash.canceled += OnDashCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        Input();

        


    }

    private void Input()
    {
        Vector2 input = InputActions.Player.Move.ReadValue<Vector2>();
        Vector2 Camerainput = InputActions.Player.Camera.ReadValue<Vector2>();
        
        // 获取相机的前方和右方向（忽略Y轴旋转）
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        
        // 根据相机方向计算移动方向
        Vector3 TargetMoveInput = (cameraForward * input.y + cameraRight * input.x);
        
        if (Mathf.Abs(TargetMoveInput.x) > 0.1f || Mathf.Abs(TargetMoveInput.z) > 0.1f)
        {
            PlayerInputData.MoveInput = Vector3.Lerp(PlayerInputData.MoveInput, TargetMoveInput, 0.01f);
        }
        else
        {
            PlayerInputData.MoveInput = Vector3.zero;
        }

        if (Mathf.Abs(Camerainput.x) > 0.1f || Mathf.Abs(Camerainput.y) > 0.1f)
        {
            PlayerInputData.CameraMove = Camerainput;
            //Debug.Log(CameraMove);
        }
        else
        {
            PlayerInputData.CameraMove = Vector2.zero;
        }


    }

    void OnDestory()
    {
        InputActions.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        PlayerInputData.JumpInput = new Vector3(0f, 1f, 0f);
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        PlayerInputData.JumpInput = Vector3.zero;
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        PlayerInputData.DashInput = true;
    }

    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        PlayerInputData.DashInput = false;
    }

}
