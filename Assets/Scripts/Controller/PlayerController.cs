using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    private Rigidbody rigidbody3d;

    void Awake()
    {
        rigidbody3d = GetComponent<Rigidbody>();
        PlayerStatusData.Thrust = playerData.Thrust;
        PlayerStatusData.Drag = playerData.Drag;
        PlayerStatusData.Energy = playerData.Energy;
        PlayerStatusData.rotationSpeed = playerData.rotationSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody3d.AddForce(FunctionDataBase.CalculateMovementForce(), ForceMode.Force);
        PlayerStatusData.Velocity = rigidbody3d.velocity;

        // 获取输入的水平值 (比如 CameraInput.x 是水平输入)
        float horizontalInput = PlayerInputData.CameraMove.x;

        // 计算旋转角度
        float rotationAmount = horizontalInput * PlayerStatusData.rotationSpeed * Time.deltaTime;

        // 绕 Y 轴旋转玩家
        transform.Rotate(0, rotationAmount, 0);

    }
}
